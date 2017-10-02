using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace task3_1
{
	public partial class MainForm : Form
	{
		char choose = ' ';
		Point prev = new Point(0,0);
		bool can_draw = false;
		Bitmap bmp, pic;
		
		public MainForm()
		{
			InitializeComponent();
			bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
		}
	}
}
