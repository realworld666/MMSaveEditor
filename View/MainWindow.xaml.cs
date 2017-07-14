using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using FullSerializer;
using GalaSoft.MvvmLight.Ioc;
using LZ4;
using Microsoft.Win32;
using MMSaveEditor.ViewModel;

namespace MMSaveEditor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private fsSerializer serializer;

        private string _openFilePath = null;
        private static readonly int saveFileVersion = 4;
        private SaveFileInfo _currentSaveInfo;

        public MainWindow()
        {
            InitializeComponent();

            this.serializer = CreateAndConfigureSerializer();
        }

        private static fsSerializer CreateAndConfigureSerializer()
        {
            return new fsSerializer() { Config = { DefaultMemberSerialization = fsMemberSerialization.OptOut, SerializeAttributes = new System.Type[1] { typeof( fsPropertyAttribute ) }, IgnoreSerializeAttributes = new System.Type[2] { typeof( NonSerializedAttribute ), typeof( fsIgnoreAttribute ) }, SerializeEnumsAsInteger = true, EnablePropertySerialization = false } };
        }

        private void open_Click( object sender, RoutedEventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Save games (*.sav)|*.*";
            openFileDialog.InitialDirectory = Path.Combine( App.Instance.LocalLowFolderPath, "Playsport Games\\Motorsport Manager\\Cloud\\Saves" );

            if( openFileDialog.ShowDialog() == true )
            {
                LoadFile( openFileDialog.FileName );
                _openFilePath = openFileDialog.FileName;
            }
        }

        private void Save_Click( object sender, RoutedEventArgs e )
        {
            if( !string.IsNullOrEmpty( _openFilePath ) )
            {
                SaveFile( _openFilePath );
            }
        }

        private void SaveFile( string openFilePath )
        {
            try
            {
                fsData data1;
                fsResult fsResult1 = this.serializer.TrySerialize<SaveFileInfo>( _currentSaveInfo, out data1 );
                if( fsResult1.Failed )
                    throw new Exception( string.Format( "Failed to serialise SaveFileInfo: {0}", (object)fsResult1.FormattedMessages ) );
                string s1 = fsJsonPrinter.CompressedJson( data1 );
                fsData data2;
                fsResult fsResult2 = this.serializer.TrySerialize<Game>( Game.Instance, out data2 );
                if( fsResult2.Failed )
                    throw new Exception( string.Format( "Failed to serialise Game: {0}",
                        (object)fsResult2.FormattedMessages ) );
                string s2 = fsJsonPrinter.CompressedJson( data2 );
                byte[] bytes1 = Encoding.UTF8.GetBytes( s1 );
                byte[] bytes2 = Encoding.UTF8.GetBytes( s2 );
                Debug.Assert( bytes1.Length < 268435456 && bytes2.Length < 268435456, "Uh-oh. Ben has underestimated how large save files might get, and we're about to save a file so large it will be detected as corrupt when loading. Best increase the limit!", null );
                byte[] buffer1 = LZ4Codec.Encode( bytes1, 0, bytes1.Length );
                byte[] buffer2 = LZ4Codec.Encode( bytes2, 0, bytes2.Length );
                FileInfo fileInfo = new FileInfo( openFilePath );
                using( FileStream fileStream = File.Create( fileInfo.FullName ) )
                {
                    using( BinaryWriter binaryWriter = new BinaryWriter( (Stream)fileStream ) )
                    {
                        binaryWriter.Write( 1932684653 );
                        binaryWriter.Write( saveFileVersion );
                        binaryWriter.Write( buffer1.Length );
                        binaryWriter.Write( bytes1.Length );
                        binaryWriter.Write( buffer2.Length );
                        binaryWriter.Write( bytes2.Length );
                        binaryWriter.Write( buffer1 );
                        binaryWriter.Write( buffer2 );
                    }
                }
            }
            catch( Exception ex )
            {
                MessageBoxResult result = MessageBox.Show( ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        private void LoadFile( string fileName )
        {
            using( FileStream fileStream = File.Open( fileName, FileMode.Open ) )
            {
                using( BinaryReader binaryReader = new BinaryReader( fileStream ) )
                {
                    if( binaryReader.ReadInt32() != 1932684653 )
                        throw new Exception( "Save file is not a valid save file for this game" );
                    int num1 = binaryReader.ReadInt32();
                    if( num1 < saveFileVersion )
                        throw new Exception( "Save file is an old format, and no upgrade path exists - must be from an old unsupported development version" );
                    if( num1 > saveFileVersion )
                        throw new Exception( "Save file version is newer than the game version! It's either corrupt, or the game executable is out of date" );
                    int headerCount = binaryReader.ReadInt32();
                    int headerOutputLength = binaryReader.ReadInt32();
                    int gameDataCount = binaryReader.ReadInt32();
                    int gameDataOutputLength = binaryReader.ReadInt32();
                    if( headerOutputLength > 268435456 )
                    {
                        throw new Exception( "Save file header size is apparently way too big - file has either been tampered with or become corrupt" );
                    }
                    if( gameDataOutputLength > 268435456 )
                    {
                        throw new Exception( "Save file game data size is apparently way too big - file has either been tampered with or become corrupt" );
                    }

                    //
                    // Load header SaveFileInfo
                    //
                    fsData headerData;
                    fsResult fsHeaderResult1 = fsJsonParser.Parse( Encoding.UTF8.GetString( LZ4Codec.Decode( binaryReader.ReadBytes( headerCount ), 0, headerCount, headerOutputLength ) ), out headerData );
                    if( fsHeaderResult1.Failed )
                    {
                        Console.Write( "Error reported whilst parsing serialized SaveFileInfo string: {0}", (object)fsHeaderResult1.FormattedMessages );
                    }

                    _currentSaveInfo = (SaveFileInfo)null;
                    fsResult fsHeaderResult2 = this.serializer.TryDeserialize<SaveFileInfo>( headerData, ref _currentSaveInfo );
                    if( fsHeaderResult2.Failed )
                    {
                        Console.Write( "Error reported whilst deserializing SaveFileInfo: {0}", (object)fsHeaderResult2.FormattedMessages );
                    }
                    FileInfo fileInfo = new FileInfo( fileName );
                    _currentSaveInfo.fileInfo = fileInfo;

                    //
                    // Load main save data
                    // 
                    string json = Encoding.UTF8.GetString( LZ4Codec.Decode( binaryReader.ReadBytes( gameDataCount ), 0, gameDataCount, gameDataOutputLength ) );
                    File.WriteAllText( @"saveFileJSON.txt", json );
                    //SaveData saveData = JsonConvert.DeserializeObject<SaveData>( json );
                    //string formattedJSON = JsonConvert.SerializeObject( parsedJson, Formatting.Indented );

                    Game targetGame = null;
                    fsData gameData;
                    fsResult fsResult1 = fsJsonParser.Parse( json, out gameData );
                    if( fsResult1.Failed )
                    {
                        Console.Write( "Error reported whilst parsing serialized Game data string: {0}", (object)fsResult1.FormattedMessages );
                    }

                    fsResult fsResult2 = new fsResult();
                    try
                    {
                        fsResult2 = this.serializer.TryDeserialize<Game>( gameData, ref targetGame );
                        if( fsResult2.Failed )
                        {
                            Console.Write( "Error reported whilst deserializing Game data: {0}", (object)fsResult2.FormattedMessages );
                        }
                    }
                    catch( Exception ex )
                    {
                        MessageBoxResult result = MessageBox.Show( ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error );

                        foreach( object rawMessage in fsResult2.RawMessages )
                            Console.Write( rawMessage );

                        Application.Current.Shutdown();
                    }
                    foreach( object rawMessage in fsResult2.RawMessages )
                        Console.Write( rawMessage );

                    var personVM = SimpleIoc.Default.GetInstance<PlayerViewModel>();
                    personVM.SetModel( targetGame.player );
                    var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
                    teamVM.SetModel( null );
                    //var gameVM = SimpleIoc.Default.GetInstance<GameViewModel>();
                    //gameVM.SetModels(parsedJson.GetValue("time") as JObject);

                    File.WriteAllText( @"saveFileJSON.txt", json );

                    /*if( fsResult1.Failed )
                        Debug.LogErrorFormat( "Error reported whilst parsing serialized Game data string: {0}", (object)fsResult1.FormattedMessages );
                    fsResult fsResult2 = this.serializer.TryDeserialize<Game>( data, ref targetGame );
                    if( fsResult2.Failed )
                        Debug.LogErrorFormat( "Error reported whilst deserializing Game data: {0}", (object)fsResult2.FormattedMessages );
                    foreach( object rawMessage in fsResult2.RawMessages )
                        Debug.LogWarning( rawMessage, (UnityEngine.Object)null );*/
                }
            }
        }

        private void TeamPage_OnListBoxUpdated( object sender, Team e )
        {
            var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
            teamVM.SetModel( e );
        }
    }
}
