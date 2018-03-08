using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ScriptModel
{
    public partial class Form1 : Form
    {
        private int sel = 0;
        private int mov = 0;
        SModel store;
        private int objH = -1;
        String filename;
        public Form1()
        {
            InitializeComponent();
            store = new SModel();
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            List<String[]> ls = store.retreive();
            foreach (String[] item in ls)
            {
                switch (item[0])
                {
                    case "ACTOR":
                        DrawActor((new Point(int.Parse(item[1]), int.Parse(item[2]))), int.Parse(item[3]), int.Parse(item[4]), item[5]);
                        break;
                    case "ACTION":
                        DrawAction((new Point(int.Parse(item[1]), int.Parse(item[2]))), int.Parse(item[3]), int.Parse(item[4]), item[5]);
                        break;
                    case "ITEM":
                        DrawItem((new Point(int.Parse(item[1]), int.Parse(item[2]))), int.Parse(item[3]), int.Parse(item[4]), item[5]);
                        break;
                    case "TOGGLE":
                        DrawToggle((new Point(int.Parse(item[1]), int.Parse(item[2]))), int.Parse(item[3]), int.Parse(item[4]), item[5]);
                        break;
                    default:
                        Console.WriteLine("Draw object type mislabeled");
                        break;
                }
            }
        }



        private void DrawActor(Point po, int w, int h, String l)
        {
            Graphics g = panel1.CreateGraphics(); ;
            GraphicsPath path = new GraphicsPath();
            Pen p = new System.Drawing.Pen(Color.Black, 2);
            Size s = new Size(w, h);
            g.DrawLine(p, new Point(po.X + (w / 2), po.Y + h), new Point(po.X + (w / 2), 9999));
            g.DrawRectangle(p, new Rectangle(po, s));
            path.AddRectangle(new Rectangle(po, s));
            PathGradientBrush pgb = new PathGradientBrush(path);
            pgb.CenterColor = Color.LightSkyBlue;
            Color[] sc = new Color[1];
            sc[0] = Color.PowderBlue;
            pgb.SurroundColors = sc;
            g.FillRectangle(pgb, new Rectangle(po.X + 1, po.Y + 1, s.Width, s.Height));
            g.DrawString(l, new Font("Arial", 16), new SolidBrush(Color.Black), po);
            p.Dispose();
            g.Dispose();
            path.Dispose();
            pgb.Dispose();
        }

        private void DrawAction(Point po, int w, int h, String l)
        {
            Graphics g = panel1.CreateGraphics(); ;
            GraphicsPath path = new GraphicsPath();
            Pen p = new System.Drawing.Pen(Color.Black, 2);
            Size s = new Size(w, h);

            SolidBrush sb = new SolidBrush(Color.Gainsboro);
            g.FillRectangle(sb, new Rectangle(new Point((po.X - 1), (po.Y + 1)), new Size(9999, h)));
            sb.Dispose();

            g.DrawEllipse(p, new RectangleF(po, s));
            path.AddEllipse(new RectangleF(po, s));
            PathGradientBrush pgb = new PathGradientBrush(path);
            pgb.CenterColor = Color.LightSkyBlue;
            Color[] sc = new Color[1];
            sc[0] = Color.PowderBlue;
            pgb.SurroundColors = sc;
            g.FillEllipse(pgb, new RectangleF(po, s));
            po.Y += 10;
            po.X += 10;
            g.DrawString(l, new Font("Arial", 16), new SolidBrush(Color.Black), po);
            path.Dispose();
            pgb.Dispose();
            p.Dispose();
            g.Dispose();

        }

        private void DrawItem(Point po, int w, int h, String l)
        {
            Graphics g = panel1.CreateGraphics(); ;
            GraphicsPath path = new GraphicsPath();
            Pen p = new System.Drawing.Pen(Color.Black, 2);
            Size s = new Size(w, h);

            SolidBrush sb = new SolidBrush(Color.Gainsboro);
            g.FillRectangle(sb, new Rectangle(new Point((po.X - 1), (po.Y + 1)), new Size(9999, h)));
            sb.Dispose();

            Point t = new Point(po.X + 75, po.Y);
            Point ll = new Point(po.X, po.Y + 25);
            Point r = new Point(po.X + 150, po.Y + 25);
            Point b = new Point(po.X + 75, po.Y + 50);
            PointF[] points = { r, t, ll, b };
            g.DrawPolygon(p, points);
            path.AddPolygon(points);
            PathGradientBrush pgb = new PathGradientBrush(path);
            pgb.CenterColor = Color.LightSkyBlue;
            Color[] sc = new Color[1];
            sc[0] = Color.PowderBlue;
            pgb.SurroundColors = sc;
            g.FillPolygon(pgb, points);
            po.Y += 10;
            po.X += 15;
            g.DrawString(l, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(po.X+25,po.Y));
            p.Dispose();
            g.Dispose();
            path.Dispose();
            pgb.Dispose();
        }

        private void DrawToggle(Point po, int w, int h, String tog)
        {
            po.X += 25;
            if (tog == "0")
            {
                Graphics g = panel1.CreateGraphics(); ;
                Pen p = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                Size s = new Size(w, h);
                g.DrawRectangle(p, new Rectangle(po.X, po.Y, s.Width, s.Height));
                Color[] sc = new Color[1];
                sc[0] = Color.PowderBlue;
                p.Dispose();
                g.Dispose();
            }
            else if (tog == "1")
            {
                Graphics g = panel1.CreateGraphics(); ;
                Pen p = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                Size s = new Size(w, h);
                g.DrawEllipse(p, new RectangleF(po.X, po.Y, s.Width, s.Height));
                Color[] sc = new Color[1];
                sc[0] = Color.PowderBlue;
                p.Dispose();
                g.Dispose();
            }
            else if (tog == "2")
            {
                Graphics g = panel1.CreateGraphics(); ;
                GraphicsPath path = new GraphicsPath();
                Pen p = new System.Drawing.Pen(Color.Black, 2);
                Size s = new Size(w, h);
                Point ul = new Point(po.X - 50, po.Y);
                Point ur = new Point(po.X - 25, po.Y + 50);
                Point lr = new Point(po.X + 75, po.Y);
                Point ll = new Point(po.X + 100, po.Y + 50);
                PointF[] points = { ul, lr, ll, ur };
                g.DrawPolygon(p, points);
                path.AddPolygon(points);
                PathGradientBrush pgb = new PathGradientBrush(path);
                pgb.CenterColor = Color.LightSkyBlue;
                Color[] sc = new Color[1];
                sc[0] = Color.PowderBlue;
                pgb.SurroundColors = sc;
                g.FillPolygon(pgb, points);
                po.Y += 10;
                po.X += 15;
                g.DrawString("EMOTION", new Font("Arial", 16), new SolidBrush(Color.Black), new Point(po.X-50,po.Y));
                p.Dispose();
                g.Dispose();
                path.Dispose();
                pgb.Dispose();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left)
            {
                if (sel != 0)
                {
                    Console.WriteLine("SEL");
                    switch (sel)
                    {

                        case 1:
                            store.addObject("ACTOR", panel1.PointToClient(Cursor.Position).X, 25, 150, 50, ShowMyDialogBox());
                            panel1.Invalidate();
                            break;
                        case 2:
                            store.addObject("ACTION", 5, panel1.PointToClient(Cursor.Position).Y, 150, 50, ShowMyDialogBox());
                            panel1.Invalidate();
                            break;
                        case 3:
                            store.addObject("ITEM", 5, panel1.PointToClient(Cursor.Position).Y, 150, 50, ShowMyDialogBox());
                            panel1.Invalidate();
                            break;

                    }
                    sel = 0;
                }

                else if (mov == 0)
                {
                    objH = store.checkArea(panel1.PointToClient(Cursor.Position));
                    Point tog = store.checkToggle(PointToClient(Cursor.Position));
                    if (tog.X != 0)
                    {
                        Console.WriteLine("TOG");
                        store.addObject("TOGGLE", tog.X, tog.Y, 50, 50, "0");
                        panel1.Invalidate();
                    }
                    else
                    {
                        mov = 1;
                    }
                }
                else if (mov == 1)
                {
                    Console.WriteLine("MOV");
                    mov = 0;
                    objH = -1;
                }

            }else
            {
                objH = store.checkArea(panel1.PointToClient(Cursor.Position));
                objH = -1;
            }
        }

        public String ShowMyDialogBox()
        {
            Form2 testDialog = new Form2();
            String label;

            testDialog.ShowDialog(this);
            label = testDialog.txtResult.Text;
            testDialog.Dispose();
            return label;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sel = 1;
            pictureBox1.BackColor = Color.Gray;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sel = 2;
            pictureBox1.BackColor = Color.Blue;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sel = 3;
            pictureBox1.BackColor = Color.Blue;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine(panel1.PointToClient(Cursor.Position));
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (objH != -1)
            {
                store.updateObjLoc(objH, panel1.PointToClient(Cursor.Position).X, panel1.PointToClient(Cursor.Position).Y);
                panel1.Invalidate();
            }
        }

    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveDiag = new SaveFileDialog();
        saveDiag.Filter = "Script Model|*.sm";
        List<String[]> wm = store.retreive();
        if (saveDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            ;
            foreach (String[] item in wm)
            {
                String conText = "";
                for (int i = 0; i < item.Length; i++)
                {
                    conText += (item[i] + ", ");
                }

                File.AppendAllText(saveDiag.FileName, conText + Environment.NewLine);
                filename = saveDiag.FileName;
            }
        }
    }

    private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            System.IO.StreamReader sr = new
            System.IO.StreamReader(openFileDialog1.FileName);
            filename = openFileDialog1.FileName;
            MessageBox.Show(sr.ReadToEnd());
            sr.Close();
        }
    }
}
}
