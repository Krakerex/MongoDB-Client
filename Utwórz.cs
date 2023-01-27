using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    public partial class Utwórz : Form
    {
        MongoClient dbClient;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public Utwórz()
        {
            InitializeComponent();
        }
        public Utwórz(MongoClient dbClient)
        {
            InitializeComponent();
            this.dbClient = dbClient;
        }

        private void Utwórz_Load(object sender, EventArgs e)
        {
            database = dbClient.GetDatabase("Firma");

        }

        private void dodaj_pracownik_Click(object sender, EventArgs e)
        {
            collection = database.GetCollection<BsonDocument>("Pracownicy");
            var document = new BsonDocument
            {
                {"Imie", pracownik_imie_textbox.Text },
                {"Nazwisko", pracownik_nazwisko_textbox.Text },
                {"Stanowisko", pracownik_stanowisko_textbox.Text },
                {"Data_urodzenia", pracownik_data_urodzenia_textbox.Text },
                {"Adres", new BsonDocument
                    {
                    {"Miasto",pracownik_miasto_textbox.Text },
                    {"Ulica",pracownik_ulica_textbox.Text },
                    {"Nr_domu",pracownik_nr_domu_textbox.Text}
                    }
                }

            };
            collection.InsertOne(document);

        }

        private void klient_dodaj_Click(object sender, EventArgs e)
        {
            collection = database.GetCollection<BsonDocument>("Klienci");
            var document = new BsonDocument
            {
                {"Imie", klient_imie_textbox.Text },
                {"Nazwisko", klient_nazwisko_textbox.Text },
                {"Data_urodzenia", klient_data_urodzenia_textbox.Text },
                {"Adres", new BsonDocument
                    {
                    {"Miasto",klient_miasto_textbox.Text },
                    {"Ulica",klient_ulica_textbox.Text },
                    {"Nr_domu,",klient_nr_domu_textbox.Text}
                    }
                }

            };
            collection.InsertOne(document);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            collection = database.GetCollection<BsonDocument>("Produkty");
            var document = new BsonDocument
            {
                {"Nazwa", produkt_nazwa_textbox.Text },
                {"Typ", produkt_typ_textbox.Text },
                {"Data Produkcji", klient_data_urodzenia_textbox.Text },
                {"Cena",produkt_cena_textbox.Text}
                

            };
            collection.InsertOne(document);
        }
    }
}
