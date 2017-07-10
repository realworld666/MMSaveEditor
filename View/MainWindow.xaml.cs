using System;
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

        public MainWindow()
        {
            InitializeComponent();

            this.serializer = CreateAndConfigureSerializer();
        }

        private static fsSerializer CreateAndConfigureSerializer()
        {
            return new fsSerializer() { Config = { DefaultMemberSerialization = fsMemberSerialization.OptOut, SerializeAttributes = new System.Type[1] { typeof(fsPropertyAttribute) }, IgnoreSerializeAttributes = new System.Type[2] { typeof(NonSerializedAttribute), typeof(fsIgnoreAttribute) }, SerializeEnumsAsInteger = true } };
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Save games (*.sav)|*.*";
            openFileDialog.InitialDirectory = Path.Combine(App.Instance.LocalLowFolderPath, "Playsport Games\\Motorsport Manager\\Cloud\\Saves");

            if (openFileDialog.ShowDialog() == true)
            {
                LoadFile(openFileDialog.FileName);
            }
        }

        private void LoadFile(string fileName)
        {
            using (FileStream fileStream = File.Open(fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    if (binaryReader.ReadInt32() != 1932684653)
                        throw new Exception("Save file is not a valid save file for this game");
                    int saveFileVersion = binaryReader.ReadInt32();
                    //if( num1 < SaveSystem.saveFileVersion )
                    //  throw new SaveException( "Save file is an old format, and no upgrade path exists - must be from an old unsupported development version" );
                    //if( num1 > SaveSystem.saveFileVersion )
                    //  throw new SaveException( "Save file version is newer than the game version! It's either corrupt, or the game executable is out of date" );
                    int count = binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    int num2 = binaryReader.ReadInt32();
                    int outputLength = binaryReader.ReadInt32();
                    if (outputLength > 268435456)
                    {
                        throw new Exception("Save file game data size is apparently way too big - file has either been tampered with or become corrupt");
                    }

                    binaryReader.ReadBytes(count);
                    string json = Encoding.UTF8.GetString(LZ4Codec.Decode(binaryReader.ReadBytes(num2), 0, num2, outputLength));

                    //SaveData saveData = JsonConvert.DeserializeObject<SaveData>( json );
                    //string formattedJSON = JsonConvert.SerializeObject( parsedJson, Formatting.Indented );

                    Game targetGame = null;
                    fsData data;
                    fsResult fsResult1 = fsJsonParser.Parse(json, out data);
                    if (fsResult1.Failed)
                        Console.Write("Error reported whilst parsing serialized Game data string: {0}", (object)fsResult1.FormattedMessages);
                    fsResult fsResult2 = new fsResult();
                    try
                    {
                        fsResult2 = this.serializer.TryDeserialize<Game>(data, ref targetGame);
                        if (fsResult2.Failed)
                            Console.Write("Error reported whilst deserializing Game data: {0}", (object)fsResult2.FormattedMessages);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);

                        foreach (object rawMessage in fsResult2.RawMessages)
                            Console.Write(rawMessage);

                        Application.Current.Shutdown();
                    }
                    foreach (object rawMessage in fsResult2.RawMessages)
                        Console.Write(rawMessage);

                    var personVM = SimpleIoc.Default.GetInstance<PlayerViewModel>();
                    personVM.SetModel(targetGame.player);
                    var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
                    teamVM.SetModel(null);
                    //var gameVM = SimpleIoc.Default.GetInstance<GameViewModel>();
                    //gameVM.SetModels(parsedJson.GetValue("time") as JObject);

                    //File.WriteAllText( @"saveFileJSON.txt", json );*/

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

        private void TeamPage_OnListBoxUpdated(object sender, Team e)
        {
            var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
            teamVM.SetModel(e);
        }
    }
}
