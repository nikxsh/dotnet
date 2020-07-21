using System;
using System.Collections.Generic;

namespace ObjectOriented.StructuralPatterns
{
    /// <summary>
    /// - Structural Patterns
    /// - A tree structure of simple and composite objects
    /// - Composite pattern is used when we need to treat a group of objects and a single object in the same way. 
    /// - Composite pattern composes objects in term of a tree structure to represent part as well as whole hierarchies.
    /// - This pattern creates a class contains group of its own objects.This class provides ways to modify its group of same objects.
    /// </summary>
    class CompositePattern
    {
        public CompositePattern()
        {
            var root = new CompositeElement("Picture");
            root.Add(new PrimitiveElement("Red Line"));
            root.Add(new PrimitiveElement("Blue Circle"));
            root.Add(new PrimitiveElement("Green Box"));

            var compositeNode = new CompositeElement("Two Circles");
            compositeNode.Add(new PrimitiveElement("Black Circle"));
            compositeNode.Add(new PrimitiveElement("White Circle"));

            root.Add(compositeNode);

            var pnode = new PrimitiveElement("Yellow Line");
            root.Add(pnode);
            root.Remove(pnode);

            root.Display(1);
        }

        /// <summary>
        /// The 'Component' Treenode
        /// </summary>
        abstract class DrawingElement
        {
            protected string _name;

            protected DrawingElement(string name)
            {
                _name = name;
            }


            public abstract void Add(DrawingElement d);
            public abstract void Remove(DrawingElement d);
            public abstract void Display(int indent);
        }

        /// <summary>
        /// The 'Leaf' class
        /// </summary>
        class PrimitiveElement : DrawingElement
        {
            public PrimitiveElement(string name) : base(name)
            {
            }

            public override void Add(DrawingElement c)
            {
                Console.WriteLine("Cannot add to a PrimitiveElement");
            }

            public override void Remove(DrawingElement c)
            {
                Console.WriteLine("Cannot remove from a PrimitiveElement");
            }

            public override void Display(int indent)
            {
                Console.WriteLine(new String('-', indent) + " " + _name);
            }
        }

        /// <summary>
        /// The 'Composite' class. It contains leaf objects
        /// </summary>
        class CompositeElement : DrawingElement
        {
            private List<DrawingElement> elements = new List<DrawingElement>();

            public CompositeElement(string name) : base(name)
            {
            }

            public override void Add(DrawingElement d)
            {
                elements.Add(d);
            }

            public override void Remove(DrawingElement d)
            {
                elements.Remove(d);
            }

            public override void Display(int indent)
            {
                Console.WriteLine(new String('-', indent) + "+ " + _name);

                // Display each child element on this node
                foreach (DrawingElement d in elements)
                {
                    d.Display(indent + 2);
                }
            }
        }
    }
}
