using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Engine
{
    private readonly List<Connection> connections;
    private readonly List<Location> locations;

    public Engine()
    {
        connections = new List<Connection>();
        locations = new List<Location>();
    }

    public List<Location> Locations
    {
        get
        {
            return locations;
        }
    }

    public List<Connection> Connections
    {
        get { return connections; }
    }

    public Route Calculate(Location startLocation, Location endLocation)
    {
        var shortestPaths = new Dictionary<Location, Route>();
        var handledLocations = new List<Location>();

        foreach (var location in locations)
        {
            shortestPaths.Add(location, new Route(location.Identifier));
        }

        shortestPaths[startLocation].Cost = 0;

        while (handledLocations.Count != locations.Count)
        {
            var shortestLocations = (from s in shortestPaths
                                     orderby s.Value.Cost
                                     select s.Key).ToList();

            Location locationToProcess = null;

            foreach (var location in shortestLocations)
            {
                if (!handledLocations.Contains(location))
                {
                    if (shortestPaths[location].Cost == int.MaxValue)
                        return null;
                    locationToProcess = location;
                    break;
                }
            }

            var selectedConnections = from c in connections
                                      where c.From == locationToProcess
                                      select c;
            foreach (var conn in selectedConnections)
            {
                if (shortestPaths[conn.To].Cost > conn.Distance + shortestPaths[conn.From].Cost)
                {
                    shortestPaths[conn.To].Connections = shortestPaths[conn.From].Connections.ToList();
                    shortestPaths[conn.To].Connections.Add(conn);
                    shortestPaths[conn.To].Cost = conn.Distance + shortestPaths[conn.From].Cost;

                    if (conn.To.Identifier.Equals(endLocation.Identifier))
                    {
                        return shortestPaths[conn.To];
                    }
                }
            }

            handledLocations.Add(locationToProcess);
        }

        return null;
    }
}

public class Connection
{
    private Location from, to;
    private int distance;

    public Connection(ConnectionBuilder builder)
    {
        this.from = builder.A;
        this.to = builder.B;
        this.distance = builder.Distance;
    }

    public Connection(Location from, Location to, int distance)
    {
        this.from = from;
        this.to = to;
        this.distance = distance;
    }

    public Location From
    {
        get
        {
            return @from;
        }
        set
        {
            @from = value;
        }
    }

    public Location To
    {
        get
        {
            return to;
        }
        set
        {
            to = value;
        }
    }

    public int Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
        }
    }

    public class ConnectionBuilder
    {
        public Location A { get; set; }
        public Location B { get; set; }
        public int Distance { get; set; }



        public static ConnectionBuilder aConnection()
        {
            return new ConnectionBuilder();
        }

        public ConnectionBuilder From(Location a)
        {
            this.A = a;
            return this;
        }

        public ConnectionBuilder To(Location b)
        {
            this.B = b;
            return this;
        }

        public ConnectionBuilder With(int distance)
        {
            this.Distance = distance;
            return this;
        }

        public Connection Build()
        {
            return new Connection(this);
        }
    }
}

public class Location
{
    string identifier;
    public Location(string identifier)
    {
        this.identifier = identifier;
    }
    public string Identifier
    {
        get { return this.identifier; }
        set { this.identifier = value; }
    }
    public override string ToString()
    {
        return identifier;
    }


    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return this.Identifier.Equals(((Location)obj).Identifier);
        
    }

// override object.GetHashCode
    public override int GetHashCode()
    {
        return Identifier.GetHashCode();
    }
}

public class Route
{
    private int cost;
    private List<Connection> connections;
    private readonly string identifier;

    public Route(string identifier)
    {
        this.cost = int.MaxValue;
        this.connections = new List<Connection>();
        this.identifier = identifier;
    }

    public List<Connection> Connections
    {
        get
        {
            return connections;
        }
        set
        {
            connections = value;
        }
    }
    public int Cost
    {
        get
        {
            return cost;
        }
        set
        {
            cost = value;
        }
    }
    public override string ToString()
    {
        return "Id:" + identifier + " Cost:" + Cost;
    }
}
