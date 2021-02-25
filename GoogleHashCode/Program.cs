using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleHashCode
{
    class Program
    {
        static Dictionary<string, int> _schedule = new Dictionary<string, int>();

        static Dictionary<string, PathCar> pathCars = new Dictionary<string, PathCar>();

        static List<Car> carsMoving = new List<Car>();

        static double globalPoints;
        static string fileResultPath;
        static int _totalSeconds;
        static int _totalIntersections;
        static int _totalStreets;
        static int _totalCars;
        static int _successPoints;
        static List<Node> intersections = new List<Node>();
        static void Main(string[] args)
        {
            var fileLocation = args[0];
            fileResultPath = Path.Combine(Directory.GetCurrentDirectory(), args[1]);
            Initialize(fileLocation);

            Solve();

            Console.WriteLine("Points : " + globalPoints);
        }

        
        private static void Initialize(string fileLocation)
        {
            using (File.CreateText(fileResultPath))
            {

            }

            using (var file = new StreamReader(fileLocation))
            {
                var input = file.ReadLine().Split(' ');
                _totalSeconds = Convert.ToInt32(input[0]);
                _totalIntersections = Convert.ToInt32(input[1]);
                _totalStreets = Convert.ToInt32(input[2]);
                _totalCars = Convert.ToInt32(input[3]);
                _successPoints = Convert.ToInt32(input[4]);

                for (var i = 0; i < _totalIntersections; i++)
                {
                    intersections.Add(new Node(i));
                }

                for (var i = 0; i < _totalStreets; i++)
                {
                    var inputLine = file.ReadLine().Split(' ');
                    var parent = Convert.ToInt32(inputLine[0]);
                    var child = Convert.ToInt32(inputLine[1]);
                    var streetName = inputLine[2];
                    var time = Convert.ToInt32(inputLine[3]);

                    pathCars.Add(streetName, new PathCar(child, parent, time, streetName));

                    intersections[parent].Children.Add(intersections[child]);
                }


                for (var i = 0; i < _totalCars; i++)
                {
                    var inputLine = file.ReadLine().Split(' ');
                    var totalStreetsToMove = Convert.ToInt32(inputLine[0]);
                   
                    List<PathCar> streets = new List<PathCar>();

                    var timeStreets = 0;
                   
                    for(var j = 1; j <= totalStreetsToMove; i++)
                    {
                        streets.Add(pathCars[inputLine[j]]);
                        timeStreets += pathCars[inputLine[j]].Time;
                    }

                    Node beginningNode = intersections[streets.First().ParentId];
                    beginningNode.CarQueue.Enqueue(new Car(streets, timeStreets));

                }

            }
        }


        private static void Solve()
        {

            for(int i = 0; i < _totalSeconds; i++)
            {
                if (carsMoving.Any())
                {

                }

                var nodesWithCars = intersections.Where(x => x.CarQueue.Any());

                foreach(var x in nodesWithCars)
                {
                    x.IsGreen = true;
                    var carMoving = x.CarQueue.Dequeue();
                    carMoving.PathList.First().Time--;
                    if(carMoving.PathList.First().Time == 0)
                    {
                        intersections[carMoving.PathList.First().DestinationId].CarQueue.Enqueue(carMoving);
                        carMoving.PathList.RemoveAt(0);
                    }
                    else
                    {
                        carsMoving.Add(carMoving);
                    }

                    carMoving.PathTime--;
                }

                foreach(var x in intersections.Where(x => !x.CarQueue.Any()))
                {
                    x.IsGreen = false;
                }
            
            }
        }

    }
}
