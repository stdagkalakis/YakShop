using System;
using System.Collections.Generic;

namespace YakShop.Models
{
    public class Yak
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        public Sex Sex { get; set; }
        public double LastShavedAge { get; set; }

        public Yak()
        { }

        public Yak(int id, string name, double age, Sex sex)
        {
            this.Id = id;
            this.Age = Math.Round(age, 2);
            this.Name = name;
            this.Sex = sex;
            this.LastShavedAge = 0.0;
        }

        // Return milk produced in 'days' and increase Yak Age
        public double Milk(int days)
        {
            /*
            // A yak dies when he turns 10 years (1.000 days)
            // 50- D*0,03 liters of mild (D= age in days)
            */
            double totalMilkForDays = 0.0;
            double ageInDays = Age * 100;
            for (int i = 0; i < days; i++)
            {
                if (Age + (i / 100) >= 10.0) break;  // if the Yak dies stop milking.
                totalMilkForDays += (ageInDays > 999) ? 0.0 : 50 - (ageInDays * 0.03);
                ageInDays++;
            }

            return totalMilkForDays;
        }

        // Return skins of Wool for days.
        public int Shave(int days)
        {
            /*
            // At most every 8+D*0.01 days you can again shave a LabYak (D = age in days).
            // A yak can be first shaven when he is 1 year.
            */

            // Younger that 1 year.
            if ((this.Age + (double)days / 100) < 1.0) return 0;

            // Day 0 first shave.
            int woolStack = 1;

            // Wool growing progress.
            double woolLoading = 0.0;
            for (int i = 0; i < days; i++)
            {
                if (Age + (i / 100) >= 10.0) break;  // if the Yak dies stop shaving.

                // if wool is ready for shave, shave and contnue
                if (woolLoading >= 1.0)
                {
                    woolStack++;
                    woolLoading = 0.0;
                    this.LastShavedAge += (double)i / 100;
                }
                else
                {
                    // wool increases until full and we shave it.
                    woolLoading += 1 / (8 + (Age * 100 + i) * 0.01);
                }
            }

            return woolStack;
        }

        // Update age with days
        public void IncreaseAgeByDays(int days)
        {
            this.Age += (double)days / 100;
        }

        public bool ShouldSerializeId()
        {
            return false;
        }

    }

    public enum Sex
    {
        f, m
    }


}