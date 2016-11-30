# About code

## ManaBob.Core
> `ManaBob` Project

##### See also
 - [JSON 구조 명세](https://github.com/ManaBob/Server/issues/1)

### 1. Models
#### `Repository` / `Repo<T>`
Application 특성상 다수의 Page와 ViewModel Layer를 직접 관리하는 것은 코드를 사용하기 어려울 수 있다. App의 동작은 ViewModel에 의해서 수행되며, 이 동작 요청은 결국 한 곳에서(UI Thread)에서 발생하기 때문에, Race condition의 가능성이 낮다.
코드의 편리함을 유지하기 위해 Service Mediator로 `Repository`를 사용한다.

주요 기능은 4가지로 구분할 수 있다.
 - Register  : C# object의 등록
 - Resolve   : C# object의 획득
 - Release   : C# object 등록 해제
 - Clear     : Resource/Responsibility 해제
 
```cs
    public class Repo<T>
    {
        public void     Register<KeyType>(T _value);
        public T        Resolve(String _key);
        public void     Release<KeyType>();
        public void     Clear();
        public Boolean  Contains<KeyType>();
    }
```


#### `Format`
C# object를 JSON 으로 Serialize하거나, JSON `string`을 C# object로 변환하는 기능을 담당한다. package에서는 static 함수를 사용해 conversion을 하고 있으므로, 향후 Race 문제가 발생할 경우 `System.Mutex` 사용이 필요할 수 있음.
```cs
    public class Format
    {
        public static T FromJson<T>(String _json)
        {
            // ...
        }

        public static String ToJson<T>(T _object)
        {
            // ...
        }
    }
```

##### Dependency
 - Newtonsoft.Json


#### `Message`
Client --> Server로 전송되는 단발성 통신.

ManaBob에서 이 형태의 통신은 채팅 메세지밖에 없으므로, `From`, `To`등의 Property를 사용한다. 현재 각각의 의미는 다음과 같다.
 - `From` : 발송자 User
 - `To`   : 메세지를 수신하는 Room


#### `Notification`
Server --> Client로 전송되는 단발성 통신.

현재 사용되고 있지 않음. `Content`는 또다른 JSON일 가능성이 있으며, 차후 확장 과정에서 추가 Property가 생길 수 있음. 

예상 용도
 - 유저의 방 이탈 (Leave Room)
 - 방장에 의한 방 닫기 (Close Room)

#### `Request` / `Response`
Client는 `Request`를 전송하면, Server는 `Response`를 전송한다.

1:1로 대응되며, 서로 대응 여부를 평가하는 것은 `Sender`, `Type`, `TimeStamp`를 사용한다.

`Request.Content`는 별도의 JSON 문자열이며, `Type`에 지정된 `Request.Category`에 따라서 해석방법이 달라진다.

`Response`는 `Request`의 성공여부(`Success`) 그 내용(`Reason`)으로 구성되며, Client측에서는 `Response.Type`에 따라 `Response.Reason`을 해석하여 처리할 수 있다.


#### `Room`
채팅방.

각각을 위한 고유식별자와 방의 이름, 현재 방의 상태, 인원 수 등의 정보를 가지고 있다. 채팅방의 ID는 Server-side에서 결정된다. 

가령, `Room`을 생성하고자 하면 `Request`를 발송하여야 하며, 이에 대한 `Response`에는 유의미(Valid)한 `Room.ID`가 포함되어야 한다.

```json
{
    "ID"        : 57365102,
    "Name"      : "12시 파파이스 충무로",
    "Status"    : 0,
    "Size"      : 3,
    "Capacity"  : 4,
    "Menu"      : 0,
    "Budget"    : 6000,
    "Users"     : [
        {
            "Name": "Steve",
            "ID"  : 3027124
        },
        {
            "Name": "Paul",
            "ID"  : 2754916
        },
        {
            "Name": "Martin",
            "ID"  : 3027549
        }
    ] 
}
```

#### `User`
사용자.

자신의 이름과 이에 해당하는 ID를 가진다. `Room`과 마찬가지로 `ID`는 Server-side에서 결정되며, Login `Request`에 대한 `Response`에 유의미한 값이 포함되어있어야 한다.

```json
{
    "Name": "Martin",
    "ID"  : 3027549
}
```

#### `Spot`
> Not applied

특정 장소를 의미하는 타입


### 2. ViewModels

#### `LoginViewModel`
> Not applied

Intro/Login Page(View)의 요청을 `AppCore`와 `NetService`로 중계한다.

주요 동작은 다음과 같다.
 - Auth : 3rd Party API
 - Login : `Request`

#### `ChatRoomViewModel`
> In progress

ChatRoom Page(View)의 요청을 `AppCore`와 `NetService`로 중계한다.

주요 동작은 다음과 같다.
 - Enter Room : `Request`
 - Leave Room : `Request`
 - Send Chat Message : `Request`
 - Recv Chat Message : `Notification`


#### `CreateRoomViewModel`
> In progress

CreatRoom Page(View)의 요청을 `AppCore`와 `NetService`로 중계한다.

주요 동작은 다음과 같다.
 - Open Room : `Request`

#### `RoomListViewModel`
> In progress

RoomList Page(View)의 요청을 `AppCore`와 `NetService`로 중계한다.

주요 동작은 다음과 같다.
 - Enter Room : `Request`
 - Log-out : `Request`

#### `RoomSettingViewModel`
> Not applied 

RoomSetting Page(View)의 요청을 `AppCore`와 `NetService`로 중계한다.


### 3. Pages

##### Dependency 
 - Xamarin.Forms
 - XAML
 - Images/*.png

#### `Intro`
사용자가 최초 진입하는 Page. 

##### 미구현 사항
 - 외부 API를 사용한 Authentication
 - ID/PWD 입력을 통해 Login을 수행하게 된다.

#### `RoomList`
현재까지 Server에 개설된 Room들의 목록을 보여주는 Page. 

##### 미구현 사항
 - 특정 조건에 따라 Room들의 필터링, 재정렬이 가능하다.

#### `ChatRoom`
사용자가 다른 사용자들과 간단한 메세지를 교환할 수 있는 Page.

##### 미구현 사항
 - 선택한 ChatRoom에 진입 
   - `INetService`의 지원 필요
   - Server-side의 `Room` 관리
 - 진입 시점부터 Chat Message들을 교환가능

#### `CreateRoom`

##### 미구현 사항
 - 선택한 Option들로부터 `Request`생성
   - `INetService`의 지원 필요
   - Server-side의 `Room` 관리


### 4. Services
Xamarin.Forms를 위한 PCL(Protable Class Library)에서는 서로다른 Platform에서 공용으로 사용할 수 있는 Namespace들이 제한되어있다. 따라서 `**Service`라는 이름으로 C# `interface`를 정의하고, `AppCore`의 Construction 과정에서 의존성 주입으로 이 문제를 해결한다.

#### `INetService`
> In progress

네트워킹 관련 기능을 묶은 Service

```cs
    public interface INetService : IDisposable
    {
        Task            Send(Message _message);
        Task<Response>  Send(Request _req);
        bool            IsConnected             { get; }
        EventHandler<Notification>  OnNotify    { get; set; }
    }
```

#### `ILocalService`
> Not applied

Application이 실행되는 환경에서의 File 처리를 위한 Service

```cs
    public interface ILocalService
    {
        Stream Create(String _fname);
        void Close(System.IO.Stream _stream);
        void Delete(String _fname);
    }
```

#### `IAuthService`
> Not applied

Application이 실행되는 환경에서의 Authentication 처리를 위한 Service


### 5. Controls
Custom Control을 위한 Directory. 구현된 코드 없음

## ManaBob.UWP
> `ManaBob.UWP`/`ManaBob.Windows` Project



