# Avalonia UI 전광판 대본

1. 인사

- 안녕하세요. 닷넷데브에서 Vincent 닉네임으로 활동하는 이상준입니다. 오늘은  Avalonia UI 를 통해 라즈베리 파이에서 전광판을 개발하는 방법을 소개하겠습니다.

2. Needs

- 산업 현장에서 무사고 전광판을 많이 띄우고 있습니다. 
산업 현장에서 가장 많이 사용되는 .NET 개발 플랫폼은 아무래도 Windows Forms와 WPF 라고 생각합니다.
그런데 Winform과 WPF는 Windows에서 .NET Framework를 설치해야만 동작할 수 있습니다.
이런 Windows는 보통 Desktop PC를 어딘가에 설치해서 사용합니다.
이 전광판 하나를 띄우기 위해 Desktop을 공수하고 Windows를 설치하고 .NET Framework를 설치하는 것은 너무 번거롭습니다.
여기서 좋은 대안은 라즈베리파이를 통한 소형 PC로 공간을 활용하고 Windows가 필요하지 않은 가벼운 환경을 구성하는 것입니다.
라즈베리파이를 구하고 그곳에 리눅스 기반이자, 라즈베리파이 전용 OS인 라즈비언을 설치하고 Avalonia UI를 통한 개발을 한다면
닷넷을 활용한 리눅스 UI개발이 가능하게 됩니다.

3. Avalonia UI 소개

- Avalonia UI는 닷넷 크로스 개발 플랫폼 중 하나로, Linux에서 .NET UI 개발 기술로 주로 사용되어 왔습니다.
또한 Pixel-Perfect를 추구하기 떄문에 Windows나 Linux에서 동일한 형태의 UI를 보여줍니다.
그리고 WPF의 컨트롤을 대부분 계승했기 때문에 WPF에 익숙한 개발자라면 바로 개발을 시작할 수 있습니다.
이번 행사에 Avalonia UI 세션이 많이 있으므로 자세한 설명은 생략하겠습니다.

4. 개발환경 구성

- 라즈베리파이는 많은 곳에서 구매 가능하지만 저는 엘레파츠라는 사이트를 이용했습니다.
라즈베리파이를 단품으로 구매하고 여러 부가적인 요소들은 각자 구매하는 것이 금전적으로 효과적이지만, 저는 라즈베리파이를 전문적으로 하기보다는 Avalonia UI를 빨리 테스트해보고 싶은 마음에 부가 품목이 포함된 스타터 키트를 구매했습니다.
개발환경 구성은 무척 단순합니다.
Visual Studio 2022와 .NET 6 그리고 커뮤니티 툴킷과 아발로니아 확장을 설치하면 됩니다.

5. 프로젝트 생성

- VS2022에서 Avalonia Application 프로젝트 템플릿을 선택하면 됩니다. Avalonia MVVM Application은 Reactive UI 가 기본으로 탑재되어 있어서 Reactive UI가 익숙하지 않은 분들은 적응이 어렵습니다.
따라서 저도 아직 Reactive UI를 못하기 떄문에 Avalonia Application과 커뮤니티 툴킷을 이용하여 수동으로 MVVM 환경을 구성했습니다.

- 문서에 있는 링크에 가면 Avalonia UI에서 만든 ViewLocator 소스가 있습니다.
ViewLocator를 이용하면 뷰모델과 뷰를 이름으로 매칭시켜주기 때문에 직접적인 종속관계를 제거할 수 있습니다.
완벽한 MVVM을 추구한다면 도전해볼 수 있는 방법입니다.
그 밖에 개발에 사용할 Converter, 뷰모델, 뷰 폴더를 생성합니다.

- 커뮤니티 툴깃은 널리 잘 알려져있습니다. 사용하는 방법도 무척 간단합니다.
문서의 링크에 커뮤니티 툴킷의 사용법이 있는 MSDN 문서가 있으니 확인하시면 정확하게 알고 사용하실 수 있습니다.
또한 커뮤니티 툴킷은 역사가 오래된 라이브러리입니다. 따라서 여러 버전이 있는데, 버전이 올라가면서 네이밍과 구조가 변경되었기 때문에 가급적이면 가장 최신 버전인 8버전을 사용하시는 것은 추천드립니다.

6. 본격적으로 전광판 개발

- 아까 ViewLocator를 정의한 것을 App.axaml에 포함합니다.
아발로니아에서는 이 아발로니아 자멜이라는 것을 통해 View를 개발하게 되는데 왜 자멜이 있는데 자멜을 활용하지 않고 아발로니아 자멜이라고 명명한 것을 이용하는지 링크에 토론이 있으니 확인해보시면 좋을 것 같습니다.
이제 뷰모델 폴더에 메인뷰모델과 메인윈도우뷰모델을 생성합니다.

- 메인윈도우뷰모델에 그림과 같이 코딩합니다. 커뮤니티 툴킷이 partial 키워드와 ObservableObject 속성을 사용한 클래스에 여러 코드를 더해주는데 이는 VS2022의 분석기에서 확인해볼 수 있습니다.
MainWindow는 MainView를 띄워주는 것 이외에는 아무 것도 하지 않을 것이므로 관찰 속성은 MainViewModel 하나만 선언합니다.
관찰 속성을 선언할 때는 커뮤니티 툴킷의 소스생성기의 명명규칙을 따라 소문자로 필드만 작성해줍니다.
이 MainViewModel 하나가 할당되면 ViewLocator가 MainViewModel을 MainView로 찾아서 위치시켜줍니다.

- 아까 정의한 ViewLocator는 메서드 정의를 보면 아시겠지만 ViewModel이라는 문자열을 View로 바꿔서 View를 찾습니다.
따라서 MainWindow는 View라는 접미사가 붙어있지 않기 때문에 MainWindowViewModel을 ViewLocator가 MainWindow로 못하므로, MainWindowViewModel만 따로 코드 비하인드에 할당합니다.

- 이제 MainWindow의 DataContext에 MainWindowViewModel을 할당했으므로 내부에 있는 MainViewModel 속성과 MainWindow의 컨텐츠를 바인딩해줍니다.
그리고 MainView를 Avalonia 유저 컨트롤을 생성합니다.

- 이 다음부터 MainView를 개발한 것을 보여드릴건데, 보여드리기 전에 잠깐 참고할 것이 있습니다.
WPF에서는 Resource를 통해 Style을 정의하지만, 아발로니아 유아이는 Resource 대신 Styles 를 이용해 Style을 정의합니다.
그리고 WPF에서 Animation은 스토리보드와 더블애니메이션의 클래스 연계로 애니메이션을 정의했지만, 아발로니아 유아이에서는 CSS 스타일을 따르기 때문에 키프레임을 이용하여 애니메이션을 정의합니다.
마지막으로 ViewLocator를 이용할 때 관찰객체가 Attribute형태로 적용되어 있으면 ViewLocator에서 찾지 못하므로 Locator로 찾을 뷰모델은 상속형태로 적용합니다.

7. MainView 개발

- MainView 개발을 시작하겠습니다.
제가 참고해서 개발해볼 전광판은 구글에서 **무사고 전광판**으로 검색했을 때 가장 처음에 나오는 소방서 전광판입니다.
(검색하는 거 보여준다.) https://blog.daum.net/noamall/7720163

이 전광판에는 오늘이 몇일인지 년월일이 나와있고 무사고로부터 얼마나 흘렀는지 보여주고 있는데, 저는 추가적으로 현재시각까지 보여주면서 알림말과, 초단위로 무사고 기간을 보여주겠습니다.

MainViewModel은 다음과 같이 정의했습니다.
현재 시각을 표현할 수 있도록 연 월 일 시 분 초 를 각각 관찰속성으로 정의했고, 알림말을 띄울 문자열도 관찰속성으로 정의했습니다.
그리고 전광판 프로그램의 시작시간과 현재시각을 1초 단위로 가져와서 비교하고 출력할 시간 속성도 정의했습니다.
VS2022에서 분석기를 통해 소스생성기로 생성된 소스를 확인해볼 수 있습니다.
(종속성-분석기-CommunityToolkit.Mvvm.SourceGenerators-ObservableProperty 확인)
여기 보시면 제가 필드만 선언하고 관찰가능속성만 붙인 것들이 이렇게 속성으로 정의되어있습니다.

알림판에 표현할 문자열과, 타이머도 설정했습니다.

이제 MainView입니다.
MainView의 주요한 특징은 Design.DataContext를 통해 디자인 타임에서 확인할 수 있는 ViewModel 타입을 아발로니아 자멜에서 정의하므로서 디자인타임에서도 런타임처럼 확인이 가능합니다.
Grid를 보실 때 로우 정의가 특이한 점인데, 이것은 WPF에서는 없는 것이지만, MAUI도 있고 아발로니아에도 있습니다.
아마 WPF에 이런 형태의 XAML 처리기가 없기 때문일 것인데 이후의 XAML 개발 플랫폼에도 계속해서 이런 형태가 지원이 되지 않을까 생각합니다.

나머지 Grid나 TextBlock, Canvas는 정말 WPF와 유사합니다.
그리고 보시면 Styles를 통해 Style을 정의하고 있습니다. (Canvas의 StackPanel.Styles 마우스로 동그라미)
MultiBinding 사용하는 방법도 같고, Converter를 적용하는 방법도 같습니다.

하나 또 다른 점은 Animation을 정의하는 부분인데 CSS 의 키워드로 애니메이션을 적용하도록 되어있습니다.
WPF만 하셨던 분들은 이게 뭔가 할 수 있겠지만 CSS를 다뤄보신 분들이시라면 어렵지 않게 애니메이션을 적용하실 수 있습니다.
아발로니아의 주요한 특징 중 하나가 CSS 선택자 방식으로 Style을 계층적으로 적용할 수 있다는 점입니다.
개인적으로 WPF의 고유한 계층형 스타일 적용법 보다는 CSS 형태가 좀 더 명시적인 것 같아서 이 점은 좋은 점이라고 생각합니다.

이 소스에 등장하는 생소한 부분들은 모두 아발로니아 공식 문서에 있습니다.
이 소스가 공유되니까 소스와 함께 아발로니아 문서를 보시면 쉽게 이해가능할 것이라 생각합니다.

한번 실행해보겠습니다. (소스 디버그)
지금 실행하는 이 화면을 잘 기억해두셨다가 이따가 라즈베리파이에서 실행했을 때 화면이 다른지 비교해보시기 바랍니다.

다음은 배포하는 방법인데, 게시 프로필을 설정하고 다음과 같이 설정하고 (VS2022 게시 선택)
게시를 누르면 해당 경로에 아발로니아 어셈블리가 배포됩니다.
그리고 먼저 SSH로 라즈베리파이로의 접속을 한번 확인해 봅니다.
현재 제 옆에 라즈베리파이가 있고 집 안의 사설 와이파이로 같은 네트워크 상에 있는 상태입니다.
게시경로로 가서 Windows 터미널을 실행하고 네트워크에 연결되어 있는 라즈베리파이로 SCP 명령어를 통해 파일을 전송합니다.
파일이 잘 전송되고 있습니다.

라즈베리 파이는 다른 PC기 때문에 스마트폰으로 찍어서 실행하는 방법을 보여드리도록 하겠습니다.
(스마트폰으로 라즈비언 화면 촬영)
전송이 완료되면 터미널을 통해 해당 경로로 이동한 다음
실행할 파일에 실행권한을 부여합니다. (sudo chmod +x {어셈블리명})
그리고 실행합니다. (./{어셈블리명})

보시면 아까 윈도우즈에서 보여줬던 화면과 동일합니다.
시각과 애니메이션도 제때 잘 보여주고 있습니다.

(화면 전환)

8. 끝인사

- 지금까지 아발로니아로 라즈베리파이에서 전광판 개발하기의 Vincent였습니다. 감사합니다.