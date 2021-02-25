using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleHashCode
{
    class Node
    {
        public int Id { get; set; }
        public Queue<Car> CarQueue { get; set; }
        public List<Node> Children { get; set; }
        public bool IsGreen { get; set; }

        public Node()
        {
            CarQueue = new Queue<Car>();
            Children = new List<Node>();
        }

        public Node(int id) : this()
        {
            Id = id;
        }

    }
}
