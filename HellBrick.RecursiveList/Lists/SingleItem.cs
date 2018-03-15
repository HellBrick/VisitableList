using System;
using System.Collections.Generic;

namespace HellBrick.Collections
{
	public readonly struct SingleItem<TVisitor, TItem>
		: IVisitableList<TVisitor>
		, IEquatable<SingleItem<TVisitor, TItem>>
		where TItem : IVisitable<TVisitor>
	{
		private readonly TItem _item;

		public SingleItem( TItem item ) => _item = item;

		public int Count => 1;
		public TVisitor Accept( TVisitor visitor ) => _item.Accept( visitor );

		public override int GetHashCode() => EqualityComparer<TItem>.Default.GetHashCode( _item );
		public bool Equals( SingleItem<TVisitor, TItem> other ) => EqualityComparer<TItem>.Default.Equals( _item, other._item );
		public override bool Equals( object obj ) => obj is SingleItem<TVisitor, TItem> other && Equals( other );

		public static bool operator ==( SingleItem<TVisitor, TItem> x, SingleItem<TVisitor, TItem> y ) => x.Equals( y );
		public static bool operator !=( SingleItem<TVisitor, TItem> x, SingleItem<TVisitor, TItem> y ) => !x.Equals( y );
	}
}
