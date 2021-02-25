using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleHashCode
{
    class Car
    {
        public int TotalStreetsToMove => PathList.Count;
        public List<PathCar> PathList { get; set; }
        public int PathTime { get; set; }


        public Car(List<PathCar> pathList, int time)
        {
            PathList = pathList;
            PathTime = time;
        }
    }

    class PathCar
    {
        
        public int DestinationId { get; set; }
        public int ParentId { get; set; }
        public int Time { get; set; }
        public string StreetName { get; set; }
        public PathCar(int id, int parentId, int time, string streetName)
        {
            DestinationId = id;
            ParentId = parentId;
            Time = time;
            StreetName = streetName;

        }
    }
}
