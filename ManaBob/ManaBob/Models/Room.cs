using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaBob
{

    /// <summary>
    ///      '채팅방'의 기본 클래스
    ///      - ID        : 방들의 고유 식별자
    ///      - Name      : 방 이름
    ///      - Status    : 현재 방의 상태
    ///      - Size      : 현재 인원수
    ///      - Capacity  : 최대 인원수
    ///      - Menu      : 먹을거리
    ///      - Budget    : 1인당 식비 예산
    ///      - Users     : 현재 방에 있는 사용자 목록
    /// </summary>
    /// <remarks>
    ///      ID의 충돌 방지 방안?
    ///      최대 인원수는 필수적인가?
    /// </remarks>
    public class Room
    {
        /// <summary>
        ///     식사 메뉴. 필터링에 사용
        /// </summary>
        public enum MenuCode
        {
            Unknown = 0,    // Default value
            Kor,
        }

        /// <summary>
        ///     현재 방(Room)의 상태
        /// </summary>
        public enum StatusCode
        {
            Open = 0,
            Full,
            Closed
        }

        public Int64        ID        { get; set; }
        public String       Name      { get; set; }
        public StatusCode   Status    { get; set; }
        public UInt32       Size      { get; set; }
        public UInt32       Capacity  { get; set; }
        public MenuCode     Menu      { get; set; }
        public Decimal      Budget    { get; set; }
        public List<User>   Users     { get; set; }





        /// <summary>
        ///     현재 보유한 Room 들을 특정 기준으로 필터링
        /// </summary>
        public static List<Room> Filter(List<Room> _old)
        {
            return _old;
        }

        /// <summary>
        ///     주어진 Room 들을 특정 기준으로 재정렬
        /// </summary>
        public static List<Room> Sort(List<Room> _old)
        {
            return _old;
        }

    }



}// namespace ManaBob

