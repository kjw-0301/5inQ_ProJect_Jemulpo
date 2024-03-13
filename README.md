# :sparkles: 5inQ_ProJect_Jemulpo
유니티 부트캠프.
5인큐팀 Station-J 프로젝트입니다.

## :tada: 프로젝트 소개
StationJ는 제물포 스마트 Station-J 플랫폼 개발 및 콘텐츠 구축사업의 일환으로, 사용자가 제물포 지역을 걸어다니며 이용할 수 있는 ARNavigation, 특정 위치에서 활용할 수 있는 AR포토존 및 AR도슨트를 포함합니다.

## :floppy_disk: 타겟 디바이스
- Samsung Galaxy S21

## :wrench: 개발환경
- `Unity 2022.3.2f1`
- Platform : `Android`

## :hammer: Tool 
- `C#`
- Naver Map API
- Google ARCore
- Google Cloud Platform
- Geospatial API
- 
### 1. ARCore Extension Package 설치
AR Foundation용 [ARCore Extension Package](https://developers.google.com/ar/develop/unity-arf/getting-started-extensions?hl=ko)는 Unity의 AR Foundation 패키지에 기능을 추가하여 앱에서 Cloud Anchors, 카메라 구성 필터, 녹화 및 재생과 같은 기능을 사용할 수 있습니다.

<br>

### 2. Google Cloud Platform API 사용 설정
[Google Cloud Platform](https://cloud.google.com/, "Google Cloud Platform")는 구글 클라우드 플랫폼은 구글 검색과 유튜브와 같은 최종 사용자 제품을 위해 내부적으로 구글이 사용하는, 동일한 지원 인프라스트럭처 위에서 호스팅을 제공하는 구글의 클라우드 컴퓨팅 서비스 입니다.

### Geospatial API 사용설정
Google의 [ARCore Geospatial API](https://developers.google.com/ar/develop/geospatial?hl=ko, "Google ARCore 
 Geospatial")는 Google 스트리트 뷰가 적용되는 지역의 VPS(Visual Positioning System) 기반의 현지화를 돕는 API입니다.

#### Geospatial API사용 설정
> Project Settings -> XR Plug-in Management -> ARCore Extensions -> Enable Geospatial

#### 사용 설정 및 API Key 사용 설명
Geospatial API를 사용하기 위해선 Google Cloud Platform에 프로젝트를 등록하고 API 사용설정을 하고, API Key를 발급받아야 합니다.

> Android Authentication Stratage -> API Key로 설정, Android API Key 입력

#### Geospatial Creator API 사용 설정
ARCore 및 Google Maps Platform에서 제공하는 [Geospatial Creator](https://developers.google.com/ar/geospatialcreator/intro?hl=ko, "Geospatial Creator")를 사용하면 개발자와 크리에이터 모두가 Photorealistic 3D 카드를 통해 실제 위치에서 강력하고 매력적인 3D 디지털 콘텐츠를 시각화, 빌드, 실행할 수 있습니다.

>  Project Settings -> XR Plug-in Management -> ARCore Extensions -> Enable Geospatial Creator

#### Cesium Pacakge 설치
3D 지리공간 플랫폼 [Cesium](https://cesium.com/, "Cesium")은 강력한 3D 지리 공간 응용 프로그램을 만들기 위한 기본 개방형 플랫폼입니다.

> Project Settings -> Package Manager -> Scoped Registries -> +버튼 클릭 후 내용 입력

<br>

## 담당 기능
- AR도슨트
- 동영상 촬영기능
- UI

## 주요 기능
### 1.AR Navigation
#### 프로젝트 진행은 재물포 지역이 아닌 유니티 부트캠프 주관인 경기인력 개발원 주변 위치의 POI Data로 진행 했습니다.
Naver Cloud의 Map API중 "Static Map API"를 사용했습니다. "Static Map API"는 요청된 URL 매개변수를 기반으로 웹 페이지에 표시할 수 있는 이미지로 지도를 반환 합니다. 이 때 유니티 통신 라이브러리를 통해 API 통신 및 결과 값을 받아야 Image UI에 지도를 반환할 수 있습니다. 지도에는 현재 나의 위치를 Update마다 갱신시켜 나타냅니다. 

마커에 표시되는 특정 장소들을 모두  지도의 표시된 마커의 위도와 경도는, 유니티 내에서의 비율을 계산하였고 이를 클릭하면 현재 나의 위치와 해당 위치를 "Static Map API"로 받아온 경로를 유니티의 Line Renderer로 표시합니다. 이 때 받아온 위도와 경도 즉 공공장소, 식당 등의 POI Data를 모두 JSon File로 제작하여 유니티의 JsonUtility를 사용해 POI Data를 받아왔습니다.

경로 안내를 시작하면 현재 위치를 (0,0,0)으로 AR Session이 구축되고 ARCore Library의 AREarthManager를 통해 지구를 인식하여 현재 위치의 위도 경도에 맞게 Mapping됩니다. Mapping이 완료되면 Geospatial의 GeospatialPose라는 ARCoreExtension에 있는 Struct에 위도 경도 고도를 초기화 하고, 이 Strucr를 AREarthManager.Convert() 함수 인자로 넘겨 유니티 좌표계로 치환하게되면 이 함수에서 반환된 Pose의 위치에 앵커를 생성합니다. 이 앵커들을 LineRenderer로 그려 AR네비게이션의 경로를 생성합니다. 경로를 따라 걸어가면 생성된 앵커와 사용자의 위치의 거리를 계산하여 3미터 이내에 접근하면 다음 경로를 안내해줍니다.이때 앵커에는 우회전, 좌회전 등의 방향을 설정하였습니다. 

AR네이게이션 경로 안내 중 공공장소, 식당등을 지나치게 된다면 Json으로 받아온 POI Data의 위도와 현재 위치(휴대폰 기준)가 동일하다면 POI Anchor를 생성합니다. 이 Anchor는 식당의 쿠폰 등 다양한 정보들을 제공합니다. 

### 2. AR Photozone & Docent
AR 체험존은 AR Core의 AR Tracked Image Manager와 ReferenceImageLibrary를 통해 이미지를 인식하면 그에 맞는 3D 오브젝트, VFX등 유니티내의 프리팹이 생성되고 생성된 오브젝트들과 함께 사진을 찍을 수 도 있습니다.

재물포 지역에서 사용하는 어플인 만큼 재물포내의 특정 장소를 설명하는 AR도슨트 기능도 포함되어 있습니다. AR 도슨트가 생성되는 이미지를 인식하면 AR 도슨트와 함께 도슨트 애니메이션의 진행률을 볼 수 있는 슬라이더와 다시보기 버튼이 생성되어 애니메이션을 다시 볼 수 있습니다.

### 3. 기능구현  상세 설명
#### AR 체험존 리스트 페이지 제작.
  - 리스트 페이지는 포토존과 도슨트 체험존의 POI Data를 Json으로 받아서 리스트를 생성합니다. Json에 존재하는 배열 만큼 리스트가 생기고 POI Data에 존재하는 체험존의 이름과 상세설명들 또한 받아와 리스트에 표시하도록 설계하였습니다.
  - 리스트 페이지를 통해서 상세 설명창으로 이동하게 되는데 이 때 각각의 포토존과 도슨트 페이지를 제작하게되면 너무 많은 씬이 생성되겠다고 생각했습니다. 저는 그래서 디자인 패턴인 SingleTone으로 클릭한 리스트의 Json Data를 Instance로 옮겨 저장한 뒤 상세 설명창으로 씬 이동을 할 때 Instance에 저장된 Data를 받아 씬의 이름, 상세설명 등의 Text들을 변경하도록 설계하였습니다. 이로 인해 한개의 SingleTone Instance로 AR체험존 리스트 내의 모든 체험존을 한 개의 씬으로 표현이 가능합니다.

#### 동영상 촬영기능
- Google ARcore에서 제공하는 ARCoreRecording 시스템은 AR 오브젝트와 상호작용하는 순간을 녹화하는 기능으로 사용하는 것이 불가능합니다. 제공하는 시스템은 다른 공간 즉, ARCore Session(환경)을 미리 녹화를 하면 녹화된 장소의 ARSession Data를 저장하는 기능을 합니다. 이를 활용해 PlayBack기능을 사용하면 다른 공간에서 AR을 체험하는 경험을 제공하는 기능입니다. StationJ에는 필요가 없는 기능입니다. 유니티엔진에서 생성한 AR 오브젝트는 유니티엔진의 카메라를 이용하여 렌더링 하므로 일반 휴대폰 카메라로는 녹화가 불가능합니다. 이를 해결하려면 유니티엔진에서 생성되어 유니티가 렌더링하는 AR오브젝트를 유니티의 RenderTexture를 이용해 1초에 한번 씩 캡쳐를 진행하여 이를 프레임단위로 연결하면 동영상이 만들어지지만 이는 많은 메모리와 기기의 프레임 드랍을 야기합니다. 이러한 이유로 외부 플러그인을 사용하여 영상 녹화를 진행해야한다고 판단하여 많은 플러그인을 알아봤지만 유료라는 문제점 등을 해결하지 못하여 동영상 촬영기능을 구현하지 못하였습니다.

#### AR도슨트
- 도슨트 애니메이션은 AI로 생성한 나레이션에 맞춰 제작하였습니다. AR도슨트를 재생하는 씬에는 AR 도슨트 오브젝트가 생성되면 애니메이션 진행률을 나타내는 슬라이다와 다시보기 버튼이 생성됩니다. 이는 delegate를 사용하여 생성된 도슨트 오브젝트의 애니메이션 클립과 슬라이더의 Value를 연결하여 진행률을 나타내고 다시보기 버튼에는 애니메이션 클립과 나레이션의 오디오 클립을 다시 진행하는 이벤트를 할당했습니다.

