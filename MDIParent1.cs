using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    public partial class MDIParent1 : Form
    {
       
        List<string> connectionStrings=new List<string>();
        MongoClient dbClient;
        IMongoDatabase database;
        List<String> collection;
        private int childFormNumber = 0;
        string nazwaBazy;
        Form f1;
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Okno " + childFormNumber++;
            childForm.Show();

        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    


        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            

        }
        private void setConnection(MongoClient dbClient)
        {
            this.dbClient = dbClient;
        }


       

        private void otwórzToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Wyświetl kolekcję":
                    f1 = new Dane(dbClient,database,e.ClickedItem.OwnerItem.Text, "odczyt", nazwaBazy);
                    f1.MdiParent = this;
                    f1.Show();
                    break;
                case "Edytuj kolekcję":
                    f1 = new Dane(dbClient, database,e.ClickedItem.OwnerItem.Text, "edycja", nazwaBazy);
                    f1.MdiParent = this;
                    f1.Show();
                    break;
                case "Dodaj do kolekcji":
                    f1 = new Utwórz(dbClient, database, e.ClickedItem.OwnerItem.Text, nazwaBazy);
                    f1.MdiParent = this;
                    f1.Show();
                    break;
            }
        }


        private void Bazy_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            database = dbClient.GetDatabase(e.ClickedItem.Text);
            Kolekcje.DropDownItems.Clear();
            collection = database.ListCollectionNames().ToList();
            foreach (string kolekcja in collection)
            {
                ToolStripMenuItem item;
                if (kolekcja.Contains(".mongodb"))
                {
                     item = new ToolStripMenuItem(kolekcja.Substring(0, kolekcja.Length - 8));
                }
                else
                {
                     item = new ToolStripMenuItem(kolekcja);
                }
                
                item.DropDownItems.Add("Wyświetl kolekcję");
                item.DropDownItems.Add("Edytuj kolekcję");
                item.DropDownItems.Add("Dodaj do kolekcji");
                item.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.otwórzToolStripMenuItem_DropDownItemClicked);
                Kolekcje.DropDownItems.Add(item);
                
            }



        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "") { return; }
            try
            {
                string connectBaza=toolStripTextBox1.Text;
                nazwaBazy = toolStripTextBox1.Text.Substring(toolStripTextBox1.Text.IndexOf('@') + 1, toolStripTextBox1.Text.IndexOf('.') - toolStripTextBox1.Text.IndexOf('@') - 1);

            
                if (toolStripTextBox2.Text == "")
                {
                    dbClient = new MongoClient(toolStripTextBox1.Text);
                    if (!connectionStrings.Contains(connectBaza)) toolStripDropDownButton1.DropDownItems.Add(nazwaBazy);
                    connectionStrings.Add(toolStripTextBox1.Text.ToString());
                }
                else
                {
                    dbClient = new MongoClient(toolStripTextBox1.Text.Replace("<password>", toolStripTextBox2.Text));
                    connectBaza = (toolStripTextBox1.Text.Replace("<password>", toolStripTextBox2.Text));
                    if (!connectionStrings.Contains(connectBaza)) toolStripDropDownButton1.DropDownItems.Add(nazwaBazy);
                    connectionStrings.Add((toolStripTextBox1.Text.Replace("<password>", toolStripTextBox2.Text)));
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Połączenie nieudane");
            }
            
            
            var dbList = dbClient.ListDatabases().ToList();
            Bazy.DropDownItems.Clear();
            Kolekcje.DropDownItems.Clear();
            toolStripTextBox1.Clear();
            toolStripTextBox2.Clear();
            foreach (BsonDocument baza in dbList)
            {

                foreach (BsonElement nazwa in baza)
                {
                    if (nazwa.Name == "name" && nazwa.Value.ToString() != "admin" && nazwa.Value.ToString() != "local")
                        Bazy.DropDownItems.Add(nazwa.Value.ToString());

                }
            }
           
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            foreach(string s in connectionStrings)
            {
                if(s.Substring(s.IndexOf('@') + 1, s.IndexOf('.') - s.IndexOf('@') - 1) == e.ClickedItem.Text)
                {
                    dbClient = new MongoClient(s);
                    var dbList = dbClient.ListDatabases().ToList();
                    Bazy.DropDownItems.Clear();
                    Kolekcje.DropDownItems.Clear();
                    foreach (BsonDocument baza in dbList)
                    {

                        foreach (BsonElement nazwa in baza)
                        {
                            if (nazwa.Name == "name" && nazwa.Value.ToString() != "admin" && nazwa.Value.ToString() != "local")
                                Bazy.DropDownItems.Add(nazwa.Value.ToString());

                        }
                    }
                }
            }
        }
    }
}
