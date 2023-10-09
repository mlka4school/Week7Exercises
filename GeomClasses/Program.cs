using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeomClasses
{
	internal class Program
	{
		static void Main(string[] args)
		{
			/*
			i will keep this simple with no user input but will go in depth with what one could do with inheritance using the power of comments
			at least i'll put in the bare minimum of effort with a little bit of bonus... not here though, since i am using a console app and do not want to
			ascii art by hand (that would be miserable)

			we will initialize seven objects. the shape class won't be used since we don't need it and it is only a base for the other classes.
			that being said, it is good to initialize one anyways as a control.
			*/
			// shape
			Shape shapeShape = new Shape();
			// circles
			Circle circShape1 = new Circle(5);
			Circle circShape2 = new Circle(15);
			// rectangles
			Rectangle rectShape1 = new Rectangle(5,5);
			Rectangle rectShape2 = new Rectangle(4,7);
			// triangles
			Triangle triShape1 = new Triangle(5,5,5);
			Triangle triShape2 = new Triangle(9,7,5);
			
			// place everyone into a list
			List<Shape> shapeList = new List<Shape>();

			shapeList.Add(shapeShape);
			shapeList.Add(circShape1);
			shapeList.Add(circShape2);
			shapeList.Add(rectShape1);
			shapeList.Add(rectShape2);
			shapeList.Add(triShape1);
			shapeList.Add(triShape2);

			// then ask everyone to calculate their areas+perimeters and then print them into console
			foreach (Shape shape in shapeList){
				shape.CalcAreaAndPeri();
				shape.PrintParams();
			}

			// keep in mind we can still manually call objects through the variables we set. for example one could view triShape1's area manually...
			Console.WriteLine("Manually calling triShape1's Area:");
			Console.WriteLine(triShape1.Area);

			// ... or modify RectShape1's length and ask for a do-over.
			Console.WriteLine("Changing length of rectShape1:");
			rectShape1.Length = 10;
			rectShape1.CalcAreaAndPeri();
			rectShape1.PrintParams();

			Console.WriteLine("Getting all circles and increasing radius:");
			// and of course, we can just do all of this through the list if we wanted to. take a query for example
			var Query = from Shape in shapeList where Shape is Circle select Shape;
			Query = Query.ToList();
			foreach (Circle shape in Query){
				Console.WriteLine(shape.Radius);
				shape.Radius += 5;
				Console.WriteLine(shape.Radius);
				shape.CalcAreaAndPeri();
				shape.PrintParams();
			}

			// experimentation is fun, and objects are more fun. possiblities could be endless with what you can do with inheritance...

			Console.ReadKey(); // the only user input, which will hold to end the program until any key press occurs
		}

		// Base class
		public class Shape{ // this is a parent and will be our basis for all other classes used in this program
			// shared variables
			public double Area {get;set;}
			public double Perimeter {get;set;}
			
			// shared function. only writes a line saying it's parameters in the console
			public void PrintParams(){
				Console.WriteLine("Area is "+Area.ToString()+" and Perimeter is "+Perimeter.ToString());
			}

			// another shared function calculating area and perimeter. by default it will set them to 10
			// keep in mind we won't actually be keeping this one for any of the children, this is just so if i WERE to
			// call shape's calcareaandperi the code wouldn't cause an error.

			// also, notice how we are using virtual. this will be important later
			public virtual void CalcAreaAndPeri(){
				Area = 10;
				Perimeter = 10;
			}
		}

		public class Circle : Shape{ // this is a child. unlike human children it doesn't cry a lot and it's generally a lot less annoying to work with
			// constructor specific to Circle
			public Circle(double radius){
				Radius = radius;
			}
			// circle specific variable
			public double Radius {get;set;}

			// this won't be explained for all of them, but we can overwrite methods as a child. remember how we used virtual for the Shape class?
			// if we override it, we can have it call this variant of CalcAreaAndPeri and not shape's. this is important mostly for the list we will be
			// putting all our initalized shapes into.
			public override void CalcAreaAndPeri(){
				Area = Radius*Math.PI; // POWER OF PIE
				Perimeter = (Radius*2)*Math.PI; // POWER OF PIE TWO
			}
		}

		public class Rectangle : Shape{
			public Rectangle (double width, double length){
				Width = width;
				Length = length;
			}
			public double Width {get;set;}
			public double Length {get;set;}
			public override void CalcAreaAndPeri(){
				Area = (Width*Length);
				Perimeter = (Width+Length)*2;
			}
		}

		public class Triangle : Shape{
			// triangles will use side3 as the base for area.
			public Triangle(double side1, double side2, double side3){
				Sides = new double[]{side1,side2,side3}; // will always be three sides. because why else would it be called a triangle, right?
			}
			public double[] Sides {get;set;}
			public override void CalcAreaAndPeri(){ // i really tricked myself into doing math today, huh...
				Perimeter = Sides[0]+Sides[1]+Sides[2];
				var Semiper = Perimeter/2;
				Area = Math.Sqrt(Semiper*(Semiper-Sides[0])*(Semiper-Sides[1])*(Semiper-Sides[2]));
			}
		}
	}
}
