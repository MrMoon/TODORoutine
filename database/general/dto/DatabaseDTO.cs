using System;

namespace TODORoutine.database.general.dto {

    /**
     * Main Database Transfor Layer 
     * Handles basic transfor operations
     **/
    public interface DatabaseDTO<T> {
        T getTById(String id);
        bool saveT(T t);
        bool deleteT(T t);
        bool updateT(T t , params String[] columns);
    }
}
