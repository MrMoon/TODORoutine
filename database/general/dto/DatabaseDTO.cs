using System;

namespace TODORoutine.database.general.dto {

    /**
     * Main Database Transfor Layer 
     * Handles basic transfor operations
     **/
    interface DatabaseDTO<T> {
        T getById(String id);
        bool save(T t);
        bool delete(String id);
        bool update(T t , params String[] columns);
    }
}
