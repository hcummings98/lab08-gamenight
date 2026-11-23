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

//History
string[] history = File.ReadAllLines(HISTORY_FILE);

int tonightNumber = history.Length + 1;
//night counter
if (history.Length == 0)
{
    Console.WriteLine("No Nights played yet.");
}
else
{
    Console.WriteLine("Nights played: " + history.Length);
//every night in the past is printed
    foreach (string night in history)
    {
        Console.WriteLine(" " + night);
    }
}

Console.WriteLine("Tonight is Night " + tonightNumber + ".");
Console.WriteLine();


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


//Part 2 

//starts below real score
int bestTonight = -1;
string winnerTonight = "";

for (int i = 0; i < players.Length; i++)
{
    Console.Write("Points tonight for " + players[i] + ": ");
    int tonight = Convert.ToInt32(Console.ReadLine());

    //keep asking until the score is in the correct range
    while (tonight < 0 || tonight > MAX_POINTS)
    {
        Console.WriteLine("Points must be from 0 to " + MAX_POINTS + ". ");
        Console.Write("Points tonight for " + players[i] + ": ");
        tonight = Convert.ToInt32(Console.ReadLine());
    }

    //change to string
    int season = Convert.ToInt32(points[i])+ tonight;
    points[i] = season.ToString();

    if (tonight > bestTonight)
    {
        
        bestTonight = tonight;
        winnerTonight = players[i];
    }

}

Console.WriteLine("Tonight's winner: " + winnerTonight + " with " + bestTonight);
Console.WriteLine();

//saves
File.WriteAllLines(POINTS_FILE, points);
Console.WriteLine("Saved " + POINTS_FILE + ".");

//Part 3

string[] entry = { HistoryEntry(tonightNumber, winnerTonight, bestTonight) };
File.AppendAllLines(HISTORY_FILE, entry);
Console.WriteLine("Added to " + HISTORY_FILE + ": " + entry[0]);


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
 
static string HistoryEntry(int night, string winner, int points)
{
    return "Night " + night + ": " + winner + " won with " + points + " points";
}