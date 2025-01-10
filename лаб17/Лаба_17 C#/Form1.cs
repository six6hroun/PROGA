using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkedListLibrary;
using ArrayListLibrary;
using System.Collections;
using System.Diagnostics;
using ZedGraph;

namespace Лаба_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] arrayListResults = new double[5];
            double[] linkedListResults = new double[5];

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    AddOperation(arrayListResults, linkedListResults);
                    break;
                case 1:
                    GetOperation(arrayListResults, linkedListResults);
                    break;
                case 2:
                    SetOperation(arrayListResults, linkedListResults);
                    break;
                case 3:
                    InsertOperation(arrayListResults, linkedListResults);
                    break;
                case 4:
                    RemoveOperation(arrayListResults, linkedListResults);
                    break;
            }
            GraphResults(arrayListResults, linkedListResults);
        }

        private void AddOperation(double[] arrayListResults, double[] linkedListResults)
        {
            int k = 0;
            var arrayList = new MyArrayList<int>();
            var linkedList = new MyLinkedList<int>();
            for (int size = 10; size <= 100000; size *= 10)
            {
                double arrayListTotalTime = 0;
                double linkedListTotalTime = 0;

                for (int i = 0; i < 20; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    for (int j = 0; j < size; j++)
                    {
                        arrayList.add(j);
                    }
                    stopwatch.Stop();
                    arrayListTotalTime += stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    for (int j = 0; j < size; j++)
                    {
                        linkedList.Add(j);
                    }
                    stopwatch.Stop();
                    linkedListTotalTime += stopwatch.ElapsedMilliseconds;
                }

                arrayListResults[k] = arrayListTotalTime / 20;
                linkedListResults[k] = linkedListTotalTime / 20;
                k++;
                arrayList.clear();
                linkedList.Clear();
            }
        }

        private void GetOperation(double[] arrayListResults, double[] linkedListResults)
        {
            int k = 0;
            var arrayList = new MyArrayList<int>();
            var linkedList = new MyLinkedList<int>();
            for (int size = 10; size <= 100000; size *= 10)
            {
                for (int j = 0; j < size; j++)
                {
                    arrayList.add(j);
                    linkedList.Add(j);
                }

                double arrayListTotalTime = 0;
                double linkedListTotalTime = 0;

                for (int i = 0; i < 20; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    for (int j = 0; j < size; j++)
                    {
                        arrayList.get(j);
                    }
                    stopwatch.Stop();
                    arrayListTotalTime += stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    for (int j = 0; j < size; j++)
                    {
                        linkedList.Get(j);
                    }
                    stopwatch.Stop();
                    linkedListTotalTime += stopwatch.ElapsedMilliseconds;
                }

                arrayListResults[k] = arrayListTotalTime / 20;
                linkedListResults[k] = linkedListTotalTime / 20;
                k++;
                arrayList.clear();
                linkedList.Clear();
            }
        }

        private void SetOperation(double[] arrayListResults, double[] linkedListResults)
        {
            int k = 0;
            var arrayList = new MyArrayList<int>();
            var linkedList = new MyLinkedList<int>();
            for (int size = 10; size <= 100000; size *= 10)
            {
                for (int j = 0; j < size; j++)
                {
                    arrayList.add(j);
                    linkedList.Add(j);
                }

                double arrayListTotalTime = 0;
                double linkedListTotalTime = 0;

                for (int i = 0; i < 20; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    for (int j = 0; j < size; j++)
                    {
                        arrayList.set(j, j * 2);
                    }
                    stopwatch.Stop();
                    arrayListTotalTime += stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    for (int j = 0; j < size; j++)
                    {
                        linkedList.Set(j, j * 2);
                    }
                    stopwatch.Stop();
                    linkedListTotalTime += stopwatch.ElapsedMilliseconds;
                }

                arrayListResults[k] = arrayListTotalTime / 20;
                linkedListResults[k] = linkedListTotalTime / 20;
                k++;
                arrayList.clear();
                linkedList.Clear();
            }
        }

        private void InsertOperation(double[] arrayListResults, double[] linkedListResults)
        {
            int k = 0;
            var arrayList = new MyArrayList<int>();
            var linkedList = new MyLinkedList<int>();
            for (int size = 10; size <= 100000; size *= 10)
            {
                double arrayListTotalTime = 0;
                double linkedListTotalTime = 0;

                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        arrayList.add(j);
                        linkedList.Add(j);
                    }

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.add(size / 2, 99999); // Вставка в середину
                    stopwatch.Stop();
                    arrayListTotalTime += stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    linkedList.Add(size / 2, 99999); // Вставка в середину
                    stopwatch.Stop();
                    linkedListTotalTime += stopwatch.ElapsedMilliseconds;

                    arrayList.clear();
                    linkedList.Clear();
                }

                arrayListResults[k] = arrayListTotalTime / 20;
                linkedListResults[k] = linkedListTotalTime / 20;
                k++;
            }
        }

        private void RemoveOperation(double[] arrayListResults, double[] linkedListResults)
        {
            int k = 0;
            var arrayList = new MyArrayList<int>();
            var linkedList = new MyLinkedList<int>();
            for (int size = 10; size <= 100000; size *= 10)
            {
                double arrayListTotalTime = 0;
                double linkedListTotalTime = 0;

                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        arrayList.add(j);
                        linkedList.Add(j);
                    }

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.removeIndex(size / 2); // Удаление из середины
                    stopwatch.Stop();
                    arrayListTotalTime += stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    linkedList.Remove(linkedList.Get(size / 2)); // Удаление из середины
                    stopwatch.Stop();
                    linkedListTotalTime += stopwatch.ElapsedMilliseconds;

                    arrayList.clear();
                    linkedList.Clear();
                }

                arrayListResults[k] = arrayListTotalTime / 20;
                linkedListResults[k] = linkedListTotalTime / 20;
                k++;
            }
        }

        private void GraphResults(double[] arrayListResults, double[] linkedListResults)
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title.Text = "Сравнение производительности ArrayList и LinkedList";
            pane.XAxis.Title.Text = "Операция";
            pane.YAxis.Title.Text = "Время (мс)";
            pane.CurveList.Clear();

            PointPairList arrayListPoints = new PointPairList();
            PointPairList linkedListPoints = new PointPairList();

            for (int i = 0; i < 5; i++)
            {
                arrayListPoints.Add(i + 1, arrayListResults[i]);
                linkedListPoints.Add(i + 1, linkedListResults[i]);
            }

            pane.AddCurve("ArrayList", arrayListPoints, Color.Blue, SymbolType.Circle);
            pane.AddCurve("LinkedList", linkedListPoints, Color.Red, SymbolType.Circle);

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
        }
    }
}
