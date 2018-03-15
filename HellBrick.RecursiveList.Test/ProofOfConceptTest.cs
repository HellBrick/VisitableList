using System;
using System.Collections.Generic;
using HellBrick.Collections;
using HellBrick.Collections.Lists;
using Xunit;

namespace HellBrick.RecursiveList.Test
{
	public class ProofOfConceptTest
	{
		[Fact]
		public void ItemTypeConstrantedListWorks()
		{
			int[] array
				= ArrayBuilder
				.Create<int>()
				.Add( 42 )
				.Add( 64 )
				.ToArray();

			Assert.Equal( array, new int[] { 42, 64 } );
		}
	}

	internal static class ArrayBuilder
	{
		public static VisitableList<Empty<Visitor<T>>, Visitor<T>> Create<T>() => VisitableList.Empty<Visitor<T>>();

		public static VisitableList<Concat<Visitor<T>, TList, SingleItem<Visitor<T>, Item<T>>>, Visitor<T>> Add<TList, T>
		(
			this VisitableList<TList, Visitor<T>> list,
			T item
		)
			where TList : struct, IVisitableList<Visitor<T>>
			=> list.Add( new Item<T>( item ) );

		public static T[] ToArray<TList, T>( this VisitableList<TList, Visitor<T>> list )
			where TList : struct, IVisitableList<Visitor<T>>
		{
			T[] array = new T[ list.List.Count ];
			Visitor<T> visitor = new Visitor<T>( array, 0 );
			return list.List.Accept( visitor ).Array;
		}

		public readonly struct Item<T> : IVisitable<Visitor<T>>, IEquatable<Item<T>>
		{
			private readonly T _item;

			public Item( T item ) => _item = item;

			public Visitor<T> Accept( Visitor<T> visitor ) => visitor.Visit( _item );

			public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode( _item );
			public bool Equals( Item<T> other ) => EqualityComparer<T>.Default.Equals( _item, other._item );
			public override bool Equals( object obj ) => obj is Item<T> other && Equals( other );

			public static bool operator ==( Item<T> x, Item<T> y ) => x.Equals( y );
			public static bool operator !=( Item<T> x, Item<T> y ) => !x.Equals( y );
		}

		public readonly struct Visitor<T> : IEquatable<Visitor<T>>
		{
			public T[] Array { get; }
			private readonly int _index;

			public Visitor( T[] array, int index )
			{
				Array = array;
				_index = index;
			}

			public Visitor<T> Visit( T item )
			{
				Array[ _index ] = item;
				return new Visitor<T>( Array, _index + 1 );
			}

			public override int GetHashCode()
			{
				unchecked
				{
					const int prime = -1521134295;
					int hash = 12345701;
					hash = hash * prime + EqualityComparer<T[]>.Default.GetHashCode( Array );
					hash = hash * prime + EqualityComparer<int>.Default.GetHashCode( _index );
					return hash;
				}
			}

			public bool Equals( Visitor<T> other ) => EqualityComparer<T[]>.Default.Equals( Array, other.Array ) && EqualityComparer<int>.Default.Equals( _index, other._index );
			public override bool Equals( object obj ) => obj is HellBrick.RecursiveList.Test.ArrayBuilder.Visitor<T> other && Equals( other );

			public static bool operator ==( Visitor<T> x, Visitor<T> y ) => x.Equals( y );
			public static bool operator !=( Visitor<T> x, Visitor<T> y ) => !x.Equals( y );
		}
	}
}
