using System;
using System.Collections.Generic;

namespace HellBrick.Collections.Lists
{
	public readonly struct Concat<TVisitor, THead, TTail>
		: IVisitableList<TVisitor>
		, IEquatable<Concat<TVisitor, THead, TTail>>
		where THead : struct, IVisitableList<TVisitor>
		where TTail : struct, IVisitableList<TVisitor>
	{
		private readonly THead _head;
		private readonly TTail _tail;

		public Concat( THead head, TTail tail )
		{
			_head = head;
			_tail = tail;
		}

		public int Count => _head.Count + _tail.Count;

		public TVisitor Accept( TVisitor visitor )
		{
			TVisitor visitorAfterHead = _head.Accept( visitor );
			TVisitor visitorAfterTail = _tail.Accept( visitorAfterHead );
			return visitorAfterTail;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				const int prime = -1521134295;
				int hash = 12345701;
				hash = hash * prime + EqualityComparer<THead>.Default.GetHashCode( _head );
				hash = hash * prime + EqualityComparer<TTail>.Default.GetHashCode( _tail );
				return hash;
			}
		}

		public bool Equals( Concat<TVisitor, THead, TTail> other ) => EqualityComparer<THead>.Default.Equals( _head, other._head ) && EqualityComparer<TTail>.Default.Equals( _tail, other._tail );
		public override bool Equals( object obj ) => obj is Concat<TVisitor, THead, TTail> other && Equals( other );

		public static bool operator ==( Concat<TVisitor, THead, TTail> x, Concat<TVisitor, THead, TTail> y ) => x.Equals( y );
		public static bool operator !=( Concat<TVisitor, THead, TTail> x, Concat<TVisitor, THead, TTail> y ) => !x.Equals( y );
	}
}
