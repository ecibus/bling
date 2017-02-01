using System;

namespace Bling.Domain
{
    public sealed class Paginator
    {
        private int m_CurrentPage;
        private int m_NoOfItems;

        public Paginator(int currentPage, int noOfItems)
        {
            m_CurrentPage = currentPage;
            m_NoOfItems = noOfItems;
        }

        public override string ToString()
        {
            string previous = "";
            string next = "";
            int totalPage = (m_NoOfItems / 10) + (m_NoOfItems % 10 > 0 ? 1 : 0);

            if (m_NoOfItems < 11)
                return "";

            if (m_CurrentPage <= totalPage)
                previous = String.Format("<a href='javascript:Page({0})'>Previous</a>", m_CurrentPage - 1);

            if (totalPage > m_CurrentPage)
                next = String.Format("<a href='javascript:Page({0})'>Next</a>", m_CurrentPage + 1);

            if (m_CurrentPage == 1)
                previous = "Previous";

            if (m_CurrentPage == totalPage)
                next = "Next";


            return String.Format("{0} {1}", previous, next);
        }

    }
}
