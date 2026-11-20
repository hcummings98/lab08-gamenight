/*
* Name: Howard Cummings
* Course: CSCI 1250, Section 001
* Assignment: Lab 08, Game Night
* Date: November 20, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/

const string PLAYERS_FILE = "players.txt";

const string POINTS_FILE = "points.txt";

const string HISTORY_FILE = "history.txt";

const int MAX_POINTS = 20;

// Look for the data files
string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");



// Part 1 The Standings
string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

//display points
for (int i = 0; i < players.Length; i++)
{
    Console.WriteLine(players[i].PadRight(11) + points[i]);
}

int leader = LeaderIndex(points);
Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
Console.WriteLine("Season total: " + TotalPoints(points));
Console.WriteLine();



//Methods


//gives back the position of the highest score
static int LeaderIndex(string[] points)
{
    int leader = 0;

    for (int i = 1; i < points.Length; i++)
    {
        if (Convert.ToInt32(points[i]) > Convert.ToInt32(points[leader]))
        {
            leader = i;
        }
    }
    return leader;
}

//gives back the sum of every score in the array
static int TotalPoints(string[] points)
{
    int total = 0;

    foreach (string value in points)
    {
        total = total + Convert.ToInt32(value);
    }
    return total;
}
