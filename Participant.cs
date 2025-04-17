public class Participant
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int[] Scores { get; set; }
    
    public int TotalScore 
    {
        get 
        {
            int sum = 0;
            foreach (int score in Scores)
            {
                sum += score;
            }
            return sum;
        }
    }
}