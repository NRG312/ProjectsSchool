using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMarker
{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;

    public PathMarker(MapLocation l, float g, float h, float f, GameObject marker, PathMarker p)
    {
        location = l;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            return location.Equals(((PathMarker)obj).location);
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}



public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Maze maze;
    [SerializeField] private Material closedMaterial;
    [SerializeField] private Material openMaterial;

    private List<PathMarker> open = new List<PathMarker>();
    private List<PathMarker> closed = new List<PathMarker>();

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject pathP;

    private PathMarker goalNode;
    private PathMarker startNode;

    private PathMarker lastPos;
    private bool done = false;

    private void RemoveAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
        foreach (var x in markers)
        {
            Destroy(x);
        }
    }

    private void BeginSearch()
    {
        done = false;
        RemoveAllMarkers();

        List<MapLocation> loc = new List<MapLocation>();
        for (int i = 0; i < maze.depth - 1; i++)
        {
            for (int j = 0; j < maze.width - 1; j++)
            {
                if (maze.map[i,j] != 1)
                {
                    loc.Add(new MapLocation(i,j));
                }
            }
        }
        loc.Shuffle();

        Vector3 startLoc = new Vector3(loc[0].x * maze.scale,0, loc[0].z * maze.scale);
        startNode = new PathMarker(new MapLocation(loc[0].x, loc[0].z),0,0,0, Instantiate(start,startLoc,Quaternion.identity),null);
        
        Vector3 goalLoc = new Vector3(loc[1].x * maze.scale,0, loc[1].z * maze.scale);
        goalNode = new PathMarker(new MapLocation(loc[1].x, loc[1].z),0,0,0, Instantiate(end,goalLoc,Quaternion.identity),null);
        
        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPos = startNode;
    }

    private void Search(PathMarker thisNode)
    {
        if (thisNode == null)
        {
            return;
        }
        if (thisNode.Equals(goalNode)) // znalezienie konca
        {
            done = true;
            return;
        }

        foreach (MapLocation loc in maze.directions)
        {
            MapLocation neighbour = loc + thisNode.location;
            if (maze.map[neighbour.x,neighbour.z] == 1)
            {
                continue;
            }

            if (neighbour.x < 1 || neighbour.x >= maze.width || neighbour.z < 1 || neighbour.z >= maze.depth)
            {
                continue;
            }

            if (isClosed(neighbour))
            {
                continue;
            }

            float G = Vector2.Distance(thisNode.location.ToVector(), neighbour.ToVector()) + thisNode.G;
            float H = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
            float F = G + H;

            GameObject pathBlock = Instantiate(pathP,
                new Vector3(neighbour.x * maze.scale, 0, neighbour.z * maze.scale), Quaternion.identity);

            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();

            values[0].text = "G: " + G.ToString("0.00");
            values[1].text = "H: " + G.ToString("0.00");
            values[2].text = "F: " + G.ToString("0.00");

            if (!UpdateMarker(neighbour,G,H,F,thisNode))
            {
                open.Add(new PathMarker(neighbour,G,H,F,pathBlock,thisNode));
            }
            
        }

        open = open.OrderBy(p => p.F).ThenBy(n => n.H).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);
        closed.Add(pm);
        
        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closedMaterial;

        lastPos = pm;
    }

    bool UpdateMarker(MapLocation map, float g, float h, float f, PathMarker p)
    {
        foreach (PathMarker path in open)
        {
            if (path.location.Equals(map))
            {
                path.G = g;
                path.H = h;
                path.F = f;
                path.parent = p;
                return true;
            }
        }

        return false;
    }
    bool isClosed(MapLocation marker)
    {
        foreach (PathMarker p in closed)
        {
            if (p.location.Equals(marker))
            {
                return true;
            }
        }

        return false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //wyszukanie pozycji na mapie i dodanie tam znacznikow
        {
            BeginSearch();
        }

        if (Input.GetKeyDown(KeyCode.B) && !done) //wyszuakiwanie drogi do celu
        {
            Search(lastPos);
        }
    }
}
