using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Input;

namespace ex1
{
	public partial class Form1 : Form
	{

        private int dir;
        private Point start;
        private Bitmap img;
        public Form1()
		{
			InitializeComponent();
			colorDialog1.Color = left.Color;
			new_image();
		}

		//private Pen right = new Pen(Color.White);
		private Pen left = new Pen(Color.Black);
		private void button3_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
				left.Color = button3.BackColor = colorDialog1.Color;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "PNG Image|*.png";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
				pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
				Text = "Рисователь - " + saveFileDialog1.FileName;
			}
		}

		
		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			start = e.Location;
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			Bitmap im = (Bitmap)pictureBox1.Image;
			if (e.Button == MouseButtons.Left)
			{
				using (Graphics g = Graphics.FromImage(im))
					g.DrawLine(left, start, e.Location);
				start = e.Location;
			}
			
			pictureBox1.Image = im;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new_image();
		}

		private void new_image()
		{
			Bitmap im = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			using (Graphics g = Graphics.FromImage(im))
				g.Clear(Color.White);

			if (pictureBox1.Image != null)
				pictureBox1.Image.Dispose();
			pictureBox1.Image = im;
			saveFileDialog1.FileName = "new.png";
		}

		
		private void button4_Click(object sender, EventArgs e)
		{
			img = (Bitmap)pictureBox1.Image;
			List<Point> points1 = find_border(2);
            List<Point> points2 = find_border(6);
            foreach (Point p in points1)
				img.SetPixel(p.X, p.Y, Color.Red); 
            
            foreach (Point p in points2)
                img.SetPixel(p.X, p.Y, Color.Red);
            pictureBox1.Image = img;
		}

		private Point Start_Point(Color start)
		{
			for (int i = img.Width - 1; i >= 0; i--)
				for (int j = 0; j < img.Height; j++)
					if (start != img.GetPixel(i, j))
						return new Point(i, j);
			return new Point(img.Width / 2, img.Height / 2);
		}

		private Point direction(Point cur, int dir)
		{
			switch (dir)
			{
				case 0: return new Point(cur.X + 1, cur.Y);
				case 1: return new Point(cur.X + 1, cur.Y - 1);
				case 2: return new Point(cur.X, cur.Y - 1);
				case 3: return new Point(cur.X - 1, cur.Y - 1);
				case 4: return new Point(cur.X - 1, cur.Y);
				case 5: return new Point(cur.X - 1, cur.Y + 1);
				case 6: return new Point(cur.X, cur.Y + 1);
				case 7: return new Point(cur.X + 1, cur.Y + 1);
				default: return cur;
			}
		}

		
		private Point next_point(Point cur, Color col)
		{
			int start_dir = (dir + 2) % 8;
			Point next_p = direction(cur, start_dir);
			int new_dir = start_dir;
			while (img.GetPixel(next_p.X, next_p.Y) != col)
			{
				if (new_dir < 0)
					new_dir += 8;
				next_p = direction(cur, new_dir);
				new_dir--;
			}
			dir = new_dir;
			return next_p;
		}

		private List<Point> find_border(int dir)
		{
			List<Point> points = new List<Point>();
			Point start_point = Start_Point(img.GetPixel(img.Width - 1, 0));
			points.Add(start_point);
			

			Color col = img.GetPixel(start_point.X, start_point.Y);
			Point next_p = next_point(start_point, col);
			points.Add(next_p);
			while (next_p != start_point)
			{
				next_p = next_point(next_p, col);
				points.Add(next_p);
			}
			return points;
		}

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
