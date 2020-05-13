using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_12
{
    public static class Program
    {
        public static List<Production> items = new List<Production>();

        public static void Main(string[] args)
        {
            string userChoice = "-1";
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:" +
                                  "\n1. Однонаправленный список" +
                                  "\n2. Двунаправленный список" +
                                  "\n3. Бинарное дерево (идеальное)" +
                                  "\n4. Стек на базе однонаправленного списка" +
                                  "\n0. Выход");
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Task1A();
                        Console.ReadLine();
                        break;
                    case "2":
                        Task1B();
                        Console.ReadLine();
                        break;
                    case "3":
                        Task1C();
                        Console.ReadLine();
                        break;
                    case "4":
                        Task2();
                        Console.ReadLine();
                        break;
                }
            } while (userChoice != "0");
        }

        public static void Task1A(int workers = 0)
        {
            Console.WriteLine("Task 1");
            Point listTask = CreateLinkedList(5);
            ShowList(listTask);

            Console.Write("Введите кол-во работников, после элемента с которыми вставится новый: ");
            if (workers == 0)
            {
                workers = int.Parse(Console.ReadLine());
            }
            else
            {
                workers = listTask.data.WorkersNumber;
            }

            Point point = listTask;
            Random rnd = new Random();
            while (point != null)
            {
                if (point.data.WorkersNumber == workers)
                {
                    Point tmp = point.next;
                    point.next = new Point(new Production {WorkersNumber = rnd.Next(0, 100)});
                    point.next.next = tmp;
                }

                point = point.next;
            }

            ShowList(listTask);

            listTask = null;
        }

        public static void Task1B()
        {
            Console.WriteLine("Task 2");
            Point doubleListTask = CreateDoubleLinkedList(5);
            ShowList(doubleListTask);

            Console.WriteLine("Удаление всех Production с четным кол-во работников.");
            Point point = doubleListTask;

            while (point.next != null)
            {
                if (point.data.WorkersNumber % 2 == 0)
                {
                    if (point.prev != null)
                    {
                        point.prev.next = point.next;
                    }
                    else
                    {
                        point = point.next;
                    }
                }

                point = point.next;
            }

            ShowList(doubleListTask);
            doubleListTask = null;
        }

        public static void Task1C()
        {
            Console.WriteLine("Task 3");
            Random rnd = new Random();
            PointTree first = new PointTree(new Production {WorkersNumber = rnd.Next(0, 100)});
            int size = 10;
            PointTree tree = IdealTree(size, first);
            ShowTree(tree, 0);
            Console.WriteLine("Высота дерева: " + Math.Ceiling(Math.Log(size, 2)));

            Console.WriteLine("Дерево поиска (справа от точки значение больше, слева - меньше): ");
            TreeToList(tree);
            tree = FindingTree();
            ShowTree(tree, 0);

            tree = null;
        }

        public static void Task2()
        {
            CustomStack stack = new CustomStack(4);
            Console.WriteLine("Стек: ");
            Console.Write(stack);

            Console.WriteLine("Добавление одного элемента в начало (тк стек) c WorkersNumber=15");
            Random rnd = new Random();
            Thread.Sleep(50);
            stack.Add(new Production {WorkersNumber = 15});
            Console.Write(stack);

            Console.WriteLine("Поиск элемента по значению");
            stack.FindByValue(15);
            Console.WriteLine();

            Console.WriteLine("Добавление нескольких элементов в начало (тк стек)");
            stack.Add(new Production {WorkersNumber = rnd.Next(0, 100)},
                new Production {WorkersNumber = rnd.Next(0, 100)});
            Console.Write(stack);

            Console.WriteLine("Удаление нескольких (3) элементов начиная с индекса (1)");
            stack.Delete(1, 3);
            Console.WriteLine("Перебор коллекции с помощью цикла foreach");
            foreach (Production item in stack)
            {
                item.ShowInfo();
            }


            stack = null;
        }

        public static Point CreateLinkedList(int length)
        {
            if (length <= 0)
            {
                Console.WriteLine("Invalid Length of the List, need to be > 0");
                return null;
            }

            Random rnd = new Random();
            Point beg = new Point(new Production {WorkersNumber = rnd.Next(0, 100)});

            Point nowPoint = beg;
            for (int i = 1; i < length; i++)
            {
                Point newPoint = new Point(new Production {WorkersNumber = rnd.Next(0, 100)});
                nowPoint.next = newPoint;
                nowPoint = newPoint;
            }

            return beg;
        }

        public static Point CreateDoubleLinkedList(int length)
        {
            if (length <= 0)
            {
                Console.WriteLine("Invalid Length of the List, need to be > 0");
                return null;
            }

            Random rnd = new Random();
            Point beg = new Point(new Production {WorkersNumber = rnd.Next(0, 100)});
            for (int i = 1; i < length; i++)
            {
                rnd = new Random();
                Point p = new Point(new Production {WorkersNumber = rnd.Next(0, 100)});
                p.next = beg;
                beg.prev = p;
                beg = p;
            }

            return beg;
        }

        public static void ShowList(Point point)
        {
            if (point == null)
            {
                Console.WriteLine("The List is empty");
                return;
            }

            Point p = point;
            while (p != null)
            {
                p.data.ShowInfo();
                p = p.next;
            }

            Console.WriteLine();
        }

        public static void ShowTree(PointTree p, int l)
        {
            if (p != null)
            {
                ShowTree(p.left, l + 3);
                for (int i = 0; i < l; i++) Console.Write(" ");
                Console.WriteLine(p.data.WorkersNumber);
                ShowTree(p.right, l + 3);
            }
        }

        public static PointTree IdealTree(int size, PointTree p)
        {
            PointTree r;
            int nl, nr;
            if (size == 0)
            {
                p = null;
                return p;
            }

            nl = size / 2;
            nr = size - nl - 1;
            Random rnd = new Random();
            Thread.Sleep(50);
            r = new PointTree(new Production {WorkersNumber = rnd.Next(0, 100)});

            r.left = IdealTree(nl, r.left);
            r.right = IdealTree(nr, r.right);
            return r;
        }

        public static void TreeToList(PointTree p)
        {
            if (p != null)
            {
                items.Add(p.data);
                TreeToList(p.left);
                TreeToList(p.right);
            }
        }

        public static PointTree FindingTree()
        {
            PointTree root = new PointTree(items[items.Count / 2]);

            foreach (Production item in items)
            {
                AddValueToFindingTree(item, root);
            }

            return root;
        }

        public static void AddValueToFindingTree(Production value, PointTree tree)
        {
            if (tree.data == null)
            {
                tree.data = value;
                return;
            }

            if (tree.data.WorkersNumber > value.WorkersNumber)
            {
                // left
                if (tree.left == null)
                {
                    tree.left = new PointTree(value);
                    return;
                }

                AddValueToFindingTree(value, tree.left);
            }
            else
            {
                // right
                if (tree.right == null)
                {
                    tree.right = new PointTree(value);
                    return;
                }

                AddValueToFindingTree(value, tree.right);
            }
        }
    }

    public class Point
    {
        public Production data; //информационное поле
        public Point next, prev;

        public Point() //конструктор без параметров
        {
            data = null;
            next = null;
            prev = null;
        }

        public Point(Production d) //конструктор с параметрами
        {
            data = d;
            next = null;
            prev = null;
        }

        public override string ToString()
        {
            return data + " ";
        }
    }

    public class PointTree
    {
        public Production data;

        public PointTree left, right;

        public PointTree()
        {
            data = null;
            left = null;
            right = null;
        }

        public PointTree(Production d)
        {
            data = d;
            left = null;
            right = null;
        }

        public override string ToString()
        {
            return data + " ";
        }
    }


    public class CustomStack : IEnumerable, IEnumerator
    {
        public int Length;
        public Point Point;
        public int position = -1;

        public CustomStack()
        {
        }

        public CustomStack(int length)
        {
            Length = length;
            Point = Program.CreateLinkedList(length);
        }

        public CustomStack(CustomStack stack)
        {
            Length = stack.Length;
            Point = stack.Point;
        }

        public void Add(Production item)
        {
            Point tmp = new Point(item);
            tmp.next = Point;
            Point = tmp;
        }

        public void Add(params Production[] items)
        {
            foreach (Production item in items)
            {
                Add(item);
            }
        }

        public void Delete(int begin, int count = 1)
        {
            Point tmp = Point;
            for (int i = 0; i < begin - 1; i++)
            {
                tmp = tmp.next;
            }

            Point tmpDelete = tmp.next;
            for (int i = 0; i < count; i++)
            {
                tmpDelete = tmpDelete.next;
            }

            tmp.next = tmpDelete;
        }

        public Production FindByValue(int workersNumber)
        {
            int count = 1;
            foreach (Production item in this)
            {
                if (item.WorkersNumber == workersNumber)
                {
                    Console.WriteLine("Элемент под номером " + count);
                    return item;
                }

                count++;
            }

            Console.WriteLine("Элемента нет в стеке");
            return null;
        }

        public CustomStack Copy()
        {
            CustomStack tmp = new CustomStack();
            tmp.Length = Length;
            tmp.Point = Point;
            return tmp;
        }

        public CustomStack Clone()
        {
            CustomStack tmp = new CustomStack();
            tmp.Length = Length;
            foreach (Production item in this)
            {
                tmp.Add((Production) item.Clone());
            }

            return tmp;
        }

        public override string ToString()
        {
            Program.ShowList(Point);
            return "";
        }

        public IEnumerator GetEnumerator()
        {
            return new CustomStackEnumerator(Length, Point);
        }

        public object Current
        {
            get
            {
                if (position == -1 || position >= Length)
                    throw new InvalidOperationException();
                Point tmp = Point;
                for (int i = 0; i < position; i++)
                {
                    tmp = tmp.next;
                }

                return tmp.data;
            }
        }

        public bool MoveNext()
        {
            if (position < Length - 1)
            {
                position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            position = -1;
        }
    }

    public class CustomStackEnumerator : IEnumerator<Production>
    {
        public int Length;
        public Point Point;
        public int position = -1;
        private Production _current;

        public CustomStackEnumerator(int length, Point point)
        {
            Point = point;
            Length = length;
        }

        public object Current
        {
            get
            {
                if (position == -1 || position >= Length)
                    throw new InvalidOperationException();
                Point tmp = Point;
                for (int i = 0; i < position; i++)
                {
                    tmp = tmp.next;
                }

                _current = tmp.data;
                return tmp.data;
            }
        }

        public bool MoveNext()
        {
            if (position < Length - 1)
            {
                position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            position = -1;
        }

        Production IEnumerator<Production>.Current => _current;

        public void Dispose()
        {
        }
    }
}