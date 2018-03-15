namespace HellBrick.Collections
{
	public interface IVisitable<TVisitor>
	{
		TVisitor Accept( TVisitor visitor );
	}
}
