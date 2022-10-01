using System.Text.Json;
using System.Text.RegularExpressions;

namespace WinForms_Homework_2_Questionnaire;

public partial class Form1 : Form
{
	public Form1()
	{
		InitializeComponent();

		dateTimePicker1.MaxDate = DateTime.Now;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		string file = $"{textBox1.Name}.json";
		if (File.Exists(file))
		{
			DialogResult result = MessageBox.Show("There is Already File Specified. Do you want to Update it?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

			if (result == DialogResult.No)
				return;
		}

		string pattern = @"^([\+]?[0-9]{1,3}[0-9]{1,12})([\s.-]?[0-9]{1,4}?)$";
		Regex regex = new Regex(pattern);

		if (!regex.IsMatch(textBox6.Text))
		{
			MessageBox.Show("Phone Number is Incorrect !", "ALERT");
			return;
		}

		var jsonStr = JsonSerializer.Serialize(new User(textBox1.Text,
				textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text,
				textBox6.Text, dateTimePicker1.Value,
				radioButton1.Checked ? Gender.Male : Gender.Female));

		File.WriteAllText(file, jsonStr);
		MessageBox.Show("Informations Successfully Added !", "ALERT");
	}

	private void button1_Click(object sender, EventArgs e)
	{
		string file = $"{textBox7.Text}.json";
		if (!File.Exists(file))
		{
			MessageBox.Show("No Informations Yet !", "ALERT");
			return;
		}

		var readJsonStr = File.ReadAllText(file);
		User? user = JsonSerializer.Deserialize<User>(readJsonStr);

		if (user == null)
		{
			MessageBox.Show("No Any User Yet !", "ALERT");
			return;
		}

		textBox1.Text = user.Name;
		textBox2.Text = user.Surname;
		textBox3.Text = user.FatherName;
		textBox4.Text = user.Country;
		textBox5.Text = user.City;
		textBox6.Text = user.PhoneNumber;
		dateTimePicker1.Value = user.BirthDate;

		if (user.Gender == Gender.Male)
			radioButton2.Checked = true;
		else
			radioButton1.Checked = true;

		MessageBox.Show("Informations Successfully Loaded !", "ALERT");
	}
}