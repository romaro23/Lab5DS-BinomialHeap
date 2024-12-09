using Lab5;
var heap = new BinomialHeap<int>();
while (true)
{
    Console.WriteLine("1 - Insert, 2 - Extract min, 3 - Get total number of nodes, 4 - Get number of trees, 5 - Print");
    int option;

    if (int.TryParse(Console.ReadLine(), out option))
    {
        int n;
        switch (option)
        {
            case 1:
                Console.WriteLine("Write number: ");
                n = int.Parse(Console.ReadLine());
                heap.Insert(n);
                break;
            case 2:
                heap.ExtractMin();
                break;
            case 3:
                Console.WriteLine($"Total nodes: {heap.GetTotalNodeCount()}");
                break;
            case 4:
                Console.WriteLine($"Total trees: {heap.GetTreeCount()}");
                break;
            case 5:
                heap.PrintHeap();
                break;
        }
    }

}