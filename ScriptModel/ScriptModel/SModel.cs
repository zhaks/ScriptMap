using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

public class SModel
{
    List<String[]> Drawings;
    List<Rectangle> objAreas;
    List<Rectangle> slArea;

    public SModel()
    {
        Drawings = new List<String[]>();
        objAreas = new List<Rectangle>();
        slArea = new List<Rectangle>();
    }

    public void addObject(String type, int x, int y, int width, int height, String label)
    {
        String x1 = x.ToString();
        String y1 = y.ToString();
        String w = width.ToString();
        String h = height.ToString();

        String[] line = new String[] { type, x1, y1, w, h, label };

        if (type != "TOGGLE")
        {
            Drawings.Add(line);
            objAreas.Add(new Rectangle(x, y, width, height));
        }
        else if (type == "TOGGLE")
        {
            bool found = false;
            foreach (String[] drawing in Drawings)
            {

                if (drawing[0] == "TOGGLE" && int.Parse(drawing[1]) == x && int.Parse(drawing[2]) == y)
                {
                    found = true;
                    if (drawing[5] == "0")
                    {
                        drawing[5] = "1";
                        break;
                    }
                    else if (drawing[5] == "1")
                    {
                        drawing[5] = "2";
                        break;
                    }
                    else if (drawing[5] == "2")
                    {
                        Drawings.Remove(drawing);
                        break;
                    }

                }
            }
            if (found == false)
            {
                Drawings.Add(line);
            }
        }
        updateSL();
    }

    public List<String[]> retreive()
    {
        return Drawings;
    }

    public int checkArea(Point cs)
    {
        List<int> collisions = new List<int>();
        int itemNo = -1;
        for (int i = 0; i < objAreas.Count; i++)
        {
            if (objAreas[i].Contains(cs.X, cs.Y))
            {
                collisions.Add(i);
            }
        }
        if (collisions.Count == 0)
        {
            itemNo = -1;
        }
        else if (collisions.Count == 1)
        {
            itemNo = collisions[0];
        }
        else if (collisions.Count >= 2)
        {
            itemNo = 0;
            int dist = Convert.ToInt32(Math.Sqrt(Math.Pow(Convert.ToDouble(Math.Abs(cs.X - objAreas[0].X)), 2) + Math.Pow(Math.Abs(Convert.ToDouble(cs.Y - objAreas[0].Y)), 2)));
            for (int re = 0; re < collisions.Count; re++)
            {
                if (Convert.ToInt32(Math.Sqrt(Math.Pow(Convert.ToDouble(Math.Abs(cs.X - objAreas[re].X)), 2) + Math.Pow(Math.Abs(Convert.ToDouble(cs.Y - objAreas[re].Y)), 2))) <= dist)
                {
                    itemNo = re;
                }
            }
        }
        return itemNo;
    }

    public void updateObjLoc(int index, int x, int y)
    {
        
        if (Drawings[index][0] == "ACTOR")
        {
            foreach (String[] drawing in Drawings)
            {
                if (drawing[0] == "TOGGLE" && drawing[1] == (int.Parse(Drawings[index][1])+25).ToString())
                {
                    drawing[1] = (x+25).ToString();                    
                }
            }
            Drawings[index][1] = x.ToString();
        }
        else if (Drawings[index][0] == "ACTION"|| Drawings[index][0] == "ITEM")
        {
            foreach (String[] drawing in Drawings)
            {
                if (drawing[0] == "TOGGLE" && drawing[2] == Drawings[index][2])
                {
                    drawing[2] = y.ToString();
                }
            }
            Drawings[index][2] = y.ToString();
        }
        else
        {
            Drawings[index][1] = x.ToString();
            Drawings[index][2] = y.ToString();
        }
        objAreas[index] = new Rectangle(int.Parse(Drawings[index][1]), int.Parse(Drawings[index][2]), int.Parse(Drawings[index][3]), int.Parse(Drawings[index][4]));
        updateSL();
    }

    public void updateSL()
    {
        slArea.Clear();
        List<int> toggleX = new List<int>();
        List<int> toggleY = new List<int>();
        foreach (Rectangle item in objAreas)
        {
            if (item.Location.Y == 25)
            {
                toggleX.Add(item.Location.X);
            }
            else if (item.Location.X == 5)
            {
                toggleY.Add(item.Location.Y);
            }
        }
        foreach (int x in toggleX)
        {
            foreach (int y in toggleY)
            {
                slArea.Add(new Rectangle(x + 25, y, 150, 50));
            }
        }
    }

    public Point checkToggle(Point po)
    {
        foreach (Rectangle rec in slArea)
        {
            if (rec.Contains(po))
            {
                return new Point(rec.Left, rec.Top);
            }
        }
        return new Point(0, 0);
    }

    public void removeToggle(int x, int y)
    {
        foreach (String[] drawing in Drawings)
        {

            if (drawing[0] == "TOGGLE" && int.Parse(drawing[1]) == x && int.Parse(drawing[2]) == y)
            {
                Drawings.Remove(drawing);
            }
        }
    }
}
