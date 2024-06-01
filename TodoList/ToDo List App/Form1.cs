using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo_List_App
{
	public partial class ToDoList : Form
	{
		public ToDoList()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		DataTable todoList = new DataTable(); //source of data
		bool isEditing = false;

		private void ToDoList_Load(object sender, EventArgs e)
		{
			// Clear columns
			todoList.Columns.Add("Title");
			todoList.Columns.Add("Description");

			// Point our datagridview to the datasource
			toDoListView.DataSource = todoList;
		}

		private void newButton_Click(object sender, EventArgs e)
		{
			titleTextBox.Text = "";
			descriptionTextBox.Text = "";
		}

		private void editButton_Click_1(Object sender, EventArgs e)
		{
			isEditing = true;

			// Fill text fields with data from table
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

		private void saveButton_Click(object sender, EventArgs e)
		{
			if(isEditing)
			{
				todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = titleTextBox.Text;
				todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = descriptionTextBox.Text;
			}
			else
			{
				todoList.Rows.Add(titleTextBox.Text, descriptionTextBox.Text);
			}
			// Clear fields 
			titleTextBox.Text = "";
			descriptionTextBox.Text = "";
			isEditing = false;
		}
	}
}
