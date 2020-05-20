using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.authentication;
using TODORoutine.database.parsers;

namespace TODORoutine.database.authentication.parser {
    /**
     * Main Authentication SQL Parser that handls Authentication SQL Statment 
     **/
    interface AuthParser : DatabaseParser<Authenticate> {

    }
}
