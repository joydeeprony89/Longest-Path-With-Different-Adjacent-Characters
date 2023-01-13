// See https://aka.ms/new-console-template for more information


// https://leetcode.com/problems/longest-path-with-different-adjacent-characters/solutions/3043377/java-solution-with-explanation/
// https://youtu.be/EA8dP63iVPk
Solution s = new Solution();
var parent = new int[7] { -1, 0, 0, 1, 1, 2, 4 };
string str = "abgceef";

var answer = s.LongestPath(parent, str);
Console.WriteLine(answer);

public class Solution
{
  public int LongestPath(int[] parent, string s)
  {
    var adj = new Dictionary<int, List<int>>();
    for (int i = 1; i < parent.Length; i++)
    {
      var c = i;
      var p = parent[i];
      if (!adj.ContainsKey(c)) adj.Add(c, new List<int>());
      adj[c].Add(p);
      if (!adj.ContainsKey(p)) adj.Add(p, new List<int>());
      adj[p].Add(c);
    }

    var visited = new HashSet<int>();
    int globalMax = 0;
    int Helper(int root)
    {
      visited.Add(root);
      // for leaf nodes
      if (!adj.ContainsKey(root)) return 1;
      int max1 = 0; int max2 = 0;
      foreach (var c in adj[root])
      {
        if (!visited.Contains(c))
        {
          var currentMax = Helper(c);
          if (s[root] == s[c]) continue;
          if (currentMax > max1)
          {
            max2 = max1;
            max1 = currentMax;
          }
          else if (currentMax > max2)
          {
            max2 = currentMax;
          }
        }
      }

      globalMax = Math.Max(globalMax, 1 + max1 + max2);
      return max1 + 1;
    }

    Helper(0);
    return globalMax;
  }
}