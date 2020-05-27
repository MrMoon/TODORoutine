﻿using System;

namespace TODORoutine.general.constants {
    /**
     * Main Type Constatnts
     **/
    class TypesConstants {
        public static readonly String FILE_TYPES = "Text Files|*.txt|All Files|*.*";
        public static Func<Boolean , Boolean> FLIP = (flag) => !flag;
    }
}
