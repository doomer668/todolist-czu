using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ToDo_List_App
{
    public partial class ToDoList : Form
    {
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
          
        }

        private void label3_Click(object sender, EventArgs e)
        {
          
        }

        DataTable todoList = new DataTable("TodoItems"); // zdroj dat
        bool isEditing = false;
        private string filePath = "todoList.xml"; // cesta k souboru pro ukládání dat

        public ToDoList()
        {
            InitializeComponent();
            InitializeDataTable();
            LoadData();
        }

        // Inicializace datové tabulky pro úkoly
        private void InitializeDataTable()
        {
            todoList.Columns.Add("Title", typeof(string));
            todoList.Columns.Add("Description", typeof(string));
        }

        // Načítání dat do tabulky při načtení formuláře
        private void ToDoList_Load(object sender, EventArgs e)
        {
            toDoListView.DataSource = todoList;
        }

        // Vymazání obsahu textových polí "title" a "description"
        private void newButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
        }

        // Vyplnění textových polí daty z tabulky pro úpravy
        private void editButton_Click_1(Object sender, EventArgs e)
        {
            isEditing = true;
            titleTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
            descriptionTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
        }

        // Odstranění vybraného řádku z tabulky
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba:" + ex);
            }
        }

        // Uložení nebo aktualizace záznamu v tabulce
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Title cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isEditing)
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = titleTextBox.Text;
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = descriptionTextBox.Text;
            }
            else
            {
                todoList.Rows.Add(titleTextBox.Text, descriptionTextBox.Text);
            }

            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
            isEditing = false;
        }

        // Uložení dat do XML souboru
        private void SaveData()
        {
            try
            {
                todoList.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepodařilo se uložit data: " + ex.Message);
            }
        }

        // Načítání dat z XML souboru
        private void LoadData()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    todoList.ReadXml(filePath);
                }
                else
                {
                    todoList.Columns.Add("Title");
                    todoList.Columns.Add("Description");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepodařilo se načíst data: " + ex.Message);
            }
        }

        // Přepsání události zavírání formuláře pro uložení dat
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveData();
            base.OnFormClosing(e);
        }
    }
}
