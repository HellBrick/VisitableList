using System;

namespace HellBrick.Collections.Lists
{
	public readonly struct Empty<TVisitor>
		: IVisitableList<TVisitor>
		, IEquatable<Empty<TVisitor>>
	{
		public int Count => 0;
		public TVisitor Accept( TVisitor visitor ) => visitor;

		public override int GetHashCode() => 0;
		public bool Equals( Empty<TVisitor> other ) => true;
		public override bool Equals( object obj ) => obj is Empty<TVisitor> other && Equals( other );

		public static bool operator ==( Empty<TVisitor> x, Empty<TVisitor> y ) => x.Equals( y );
		public static bool operator !=( Empty<TVisitor> x, Empty<TVisitor> y ) => !x.Equals( y );
	}
}
