namespace FileSearcher
{
    using System;

    public class ThreadEndedEventArgs
    {
        // ----- Variables -----

        private Boolean m_success;
        private String m_errorMsg;


        // ----- Constructor -----

        public ThreadEndedEventArgs(Boolean success,
                                    String errorMsg)
        {
            this.m_success = success;
            this.m_errorMsg = errorMsg;
        }


        // ----- Public Properties -----

        public Boolean Success
        {
            get { return this.m_success; }
        }

        public String ErrorMsg
        {
            get { return this.m_errorMsg; }
        }
    }
}
