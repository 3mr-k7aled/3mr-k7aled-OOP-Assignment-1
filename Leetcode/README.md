 solution :
public class Solution
{
    public int MaxOperations(int[] nums, int k)
    {
        Array.Sort(nums);

        int left = 0;
        int right = nums.Length - 1;
        int count = 0;

        while (left < right)
        {
            int sum = nums[left] + nums[right];

            if (sum == k)
            {
                count++;
                left++;
                right--;
            }
            else if (sum < k)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return count;
    }
}


==============================================================================

        screen of accepted : <img width="3799" height="2311" alt="Screenshot 2026-10-01 171211" src="https://github.com/user-attachments/assets/a426ae84-f8b9-4bd1-8f00-5fa4c125de8d" />


    
 

