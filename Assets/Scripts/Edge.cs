using System;

[System.Serializable]
public class Edge : IComparable<Edge> {

    public float cost;
    public Vertex vertex;


    public Edge(float cost, Vertex vertex)
    {
        this.cost = cost;
        this.vertex = vertex;
    }

    public int CompareTo(Edge other)
    {
        float result = cost - other.cost;
        int idA = vertex.GetInstanceID();
        int idB = other.vertex.GetInstanceID();
        if (idA == idB)
            return 0;
        return (int)result;
    }

    public bool Equals(Edge other)
    {
        return (other.vertex.id == this.vertex.id);
    }

    public override bool Equals(object obj)
    {
        Edge other = (Edge)obj;
        return (other.vertex.id == this.vertex.id);
    }

    public override int GetHashCode()
    {
        return this.vertex.GetHashCode();
    }
}
