using System;
using System.Collections.Generic;
using System.Text;

namespace RweedmanBabyGOAT
{
    class BabyNamesData
    {
        
        // The count is saved as a split between male names, female names, and both.
        public BabyNamesData(int maleInstances, int femaleInstances)
        {
            MaleInstances = maleInstances;
            FemaleInstances = femaleInstances;
        }

        public int MaleInstances { get; set; }

        public int FemaleInstances { get; set; }


        public int SumInstances {
            get
            {
                return MaleInstances + FemaleInstances;
            }
        }
    }
}
