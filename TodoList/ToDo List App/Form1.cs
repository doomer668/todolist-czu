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

		DataTable todoList = new DataTable("TodoItems"); //source of data
		bool isEditing = false;
        private string filePath = "todoList.xml";

        public ToDoList()
        {
            InitializeComponent();
            InitializeDataTable();
            LoadData();
        }

        private void InitializeDataTable()
        {
            todoList.Columns.Add("Title", typeof(string));
            todoList.Columns.Add("Description", typeof(string));
        }


        // vytvoří v databázi dva sloupce s názvy title a description, které pak přenese do tabulky v aplikaci.
        private void ToDoList_Load(object sender, EventArgs e)
		{

			toDoListView.DataSource = todoList;
		}

        // vymaže obsah textového pole „title“ a „description“.
        private void newButton_Click(object sender, EventArgs e)
		{
			titleTextBox.Text = "";
			descriptionTextBox.Text = "";
		}

        //Vyplnění textových polí daty z tabulky
        private void editButton_Click_1(Object sender, EventArgs e)
		{
			isEditing = true;

			titleTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
			descriptionTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error:" + ex);
			}
		}

        // přenáší data z textových polí do databáze, která jsou přenesena do tabulky v aplikaci.
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

        private void SaveData()
        {
            try
            {
                todoList.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save data: " + ex.Message);
            }
        }

        // Load data from XML file
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
                MessageBox.Show("Failed to load data: " + ex.Message);
            }
        }

        // Override form closing event to save data
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveData();
            base.OnFormClosing(e);
        }
    }
}
