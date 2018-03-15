using System;
using System.Collections.Generic;

namespace HellBrick.Collections
{
	public readonly struct VisitableList<TList, TVisitor> : IEquatable<VisitableList<TList, TVisitor>>
		where TList : IVisitableList<TVisitor>
	{
		public VisitableList( TList list ) => List = list;

		public TList List { get; }

		public override int GetHashCode() => EqualityComparer<TList>.Default.GetHashCode( List );
		public bool Equals( VisitableList<TList, TVisitor> other ) => EqualityComparer<TList>.Default.Equals( List, other.List );
		public override bool Equals( object obj ) => obj is VisitableList<TList, TVisitor> other && Equals( other );

		public static bool operator ==( VisitableList<TList, TVisitor> x, VisitableList<TList, TVisitor> y ) => x.Equals( y );
		public static bool operator !=( VisitableList<TList, TVisitor> x, VisitableList<TList, TVisitor> y ) => !x.Equals( y );
	}
}
