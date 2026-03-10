# Robot Modbus Control System

본 프로젝트는 Modbus TCP 프로토콜을 기반으로 로봇 암(Robot Arm)을 제어하고 상태를 모니터링하기 위한 C# 솔루션입니다. 
티칭 펜던트 없이 PC 환경에서 로봇의 관절 이동(MoveJ), 선형 이동(MoveL), 조그(Jog) 제어 및 다중 웨이포인트 기반의 순차 제어(Sequence Control)를 수행할 수 있도록 설계되었습니다.

<img width="807" height="488" alt="image" src="https://github.com/user-attachments/assets/df86400b-270c-4921-a37c-7fdc1f516940" />

## 개요

이 프로젝트는 **Modbus TCP**로 PLC/로봇 컨트롤러와 통신하여 로봇을 제어하는 **Windows Forms 기반 테스트 프로그램**과, 이를 구성하는 보조 라이브러리들로 이루어져 있습니다.

구성은 크게 4개 프로젝트입니다.

* **RobotCore**: Modbus 통신, 주소 변환, 로봇 포인트 패킷 인코딩
* **RobotMoveJ**: Joint Move 실행 로직
* **RobotMoveL**: Linear Move 실행 로직
* **RobotTest**: 실제 테스트용 WinForms UI 애플리케이션

이 프로그램으로 다음 작업을 수행할 수 있습니다.

* 로봇 전원/동작 비트 제어
* 정지 / E-STOP / 에러 리셋
* 현재 위치 표시
* J1~J4 조그(Jog) 제어
* 단일 MoveJ / MoveL 실행
* 현재 관절값을 리스트에 저장
* 리스트 반복 실행
* 웨이포인트 JSON 저장 / 불러오기

---

## 프로젝트 구조

```text
RobotCore/
  ModbusHelper.cs
  RcPointCodec.cs

RobotMoveJ/
  MoveJExecutor.cs

RobotMoveL/
  MoveLExecutor.cs

RobotTest/
  Program.cs
  Form1.cs
  Form1.Designer.cs
  RobotTest.csproj
  RobotTest.slnx
  waypoints.json
  packages.config
  packages/
```

추가로 `RobotTest/bin/Debug/` 아래에는 빌드된 실행 파일과 DLL이 포함되어 있습니다.

---

## 개발 환경 / 요구 사항

* **운영체제**: Windows
* **프레임워크**: .NET Framework 4.7.2
* **UI**: Windows Forms
* **통신 방식**: Modbus TCP
* **외부 패키지**: NModbus 3.0.81

### 참고

* `RobotCore`, `RobotMoveJ`, `RobotMoveL`, `RobotTest` 모두 **.NET Framework 4.7.2** 대상으로 작성되어 있습니다.
* `RobotCore`는 `NModbus.dll`을 참조합니다.
* 패키지 경로는 `RobotTest/packages/NModbus.3.0.81/...` 기준으로 연결되어 있습니다.

---

## 솔루션 구성

### 1) RobotCore

공통 기능을 담당하는 핵심 라이브러리입니다.

포함 기능:

* `ModbusHelper`

  * Modbus TCP 연결/해제
  * Coil / Register 읽기/쓰기
  * float 읽기/쓰기
  * 프로젝트 전용 주소 변환 (`SD`, `SM`, `D`, `M`)
  * 대량 레지스터 쓰기 분할 처리

* `RcPointCodec`

  * `RcPointInformation` 모델을 **100개의 Holding Register** 배열로 인코딩
  * 32비트 값(float, uint, int)의 워드 순서 처리
  * 로봇 포인트 데이터(`D0 ~ D99` 매핑용) 생성

### 2) RobotMoveJ

관절 좌표 기반 이동을 수행하는 라이브러리입니다.

* `MoveJCommand`

  * `J1`, `J2`, `J3`, `J4`
  * `Speed`, `Acc`, `Dec`

* `MoveJExecutor`

  * Joint 좌표를 `RcPointInformation`에 채움
  * `RefCoordSystem = 5`로 설정
  * 포인트 패킷을 `D1000`부터 기록
  * 실행 트리거 비트를 설정하여 이동 실행

### 3) RobotMoveL

직교 좌표 기반 선형 이동을 수행하는 라이브러리입니다.

* `MoveLCommand`

  * `X`, `Y`, `Z`
  * `Q0Degree`
  * `Speed`, `Acc`, `Dec`

* `MoveLExecutor`

  * 카테시안 좌표를 `RcPointInformation`에 채움
  * `Q0Degree`를 이용해 `Q0` 값을 계산
  * `Q1`, `Q2`, `Q3`는 0으로 설정
  * `RefCoordSystem = 0`으로 설정

### 4) RobotTest

사용자가 직접 조작하는 WinForms 테스트 프로그램입니다.

주요 기능:

* 서버 IP/Port 연결
* 로봇 상태 읽기
* 현재 좌표 표시
* Jog 버튼 제어
* MoveJ / MoveL 실행
* 웨이포인트 추가/삭제
* JSON 저장 및 불러오기
* 반복 실행

---

## 통신 개요

프로그램은 Modbus TCP로 PLC 또는 로봇 컨트롤러에 접속합니다.

기본 연결값 예시:

* IP: `192.168.0.88`
* Port: `502`
* Slave ID: `1`

실제 환경에서는 컨트롤러 설정에 맞게 변경해야 합니다.

---

## 주소 체계

코드에는 프로젝트 전용 주소 변환 로직이 포함되어 있습니다.

예:

* `D` 레지스터 → Holding Register
* `M` 비트 → Coil
* `SD`, `SM` 특수 주소도 변환 처리

즉, 코드에서 `D1000`, `M5`처럼 다루는 값을 실제 Modbus 주소로 변환하여 읽고 씁니다.

---

## MoveJ 동작 방식

`MoveJExecutor`는 다음 순서로 동작합니다.

1. Joint 명령값 생성
2. `RcPointInformation` 구조체에 값 입력
3. 포인트 패킷을 100워드 배열로 인코딩
4. `D1000` 영역에 기록
5. 실행 비트 ON
6. 완료 대기 또는 짧은 지연 후 비트 OFF

즉, 로봇이 이해하는 포인트 데이터 블록을 먼저 쓰고, 실행 비트로 시작시키는 구조입니다.

---

## MoveL 동작 방식

`MoveLExecutor`는 다음 순서로 동작합니다.

1. X/Y/Z/Q0Degree 입력
2. 내부 포인트 구조체 생성
3. 직교 좌표계 기준으로 패킷 생성
4. `D1000`부터 데이터 기록
5. 선형 이동 실행 비트 ON

---

## JSON 웨이포인트 형식

리스트에 저장한 좌표는 JSON 파일로 저장할 수 있습니다.

예시 형식:

```json
[
  {
    "J1": 0.0,
    "J2": 10.0,
    "J3": 20.0,
    "J4": 0.0
  },
  {
    "J1": 5.0,
    "J2": 15.0,
    "J3": 25.0,
    "J4": 0.0
  }
]
```

실제 저장 형식은 코드의 직렬화 구조에 따라 약간 다를 수 있으므로, 현재 `waypoints.json` 샘플도 함께 확인하는 것이 좋습니다.

---

## 실행 방법

### Visual Studio에서 실행

1. Visual Studio로 솔루션 또는 프로젝트 열기
2. NuGet/NModbus 참조 확인
3. `RobotTest`를 시작 프로젝트로 설정
4. 빌드 후 실행
5. IP/Port 설정 후 Connect

### Debug 실행 파일 사용

압축파일 안에 이미 빌드 결과물이 포함되어 있다면 다음 경로에서 실행할 수 있습니다.

* `RobotTest/bin/Debug/RobotTest.exe`

단, DLL과 설정 파일이 함께 있어야 정상 실행됩니다.

---

## 사용 순서 예시

1. 프로그램 실행
2. 컨트롤러 IP/Port 입력
3. Connect 버튼 클릭
4. Servo ON 또는 동작 준비
5. 현재 위치 확인
6. Jog 또는 MoveJ/MoveL 테스트
7. 필요한 좌표를 리스트에 저장
8. JSON으로 저장하거나 다시 불러와 반복 실행

---

## 주의 사항

* 실제 장비 연결 전 주소맵이 맞는지 반드시 확인해야 합니다.
* E-STOP, Servo ON/OFF, Start 비트 주소가 현장 PLC와 다를 수 있습니다.
* 조인트 단위와 각도 기준, Q0 회전 방향은 장비 세팅에 따라 다를 수 있습니다.
* MoveL의 자세 계산은 단순화되어 있을 수 있으므로 실제 스카라 로봇 모델에 맞는 보정이 필요할 수 있습니다.
* 테스트용 프로그램이므로 인터락, 충돌 방지, 안전 영역 확인 로직은 별도 검토가 필요합니다.

---

## 개선하면 좋은 점

* 주소맵 설정을 JSON/INI로 분리
* 통신 재시도 및 예외 로그 강화
* 현재 알람 코드 표시 기능 추가
* Move 완료 확인 로직 개선
* 웨이포인트에 이름/속도/가감속 포함
* SCARA 전용 자세 계산 보강
* 반복 실행 시 취소/일시정지 기능 추가

---

## 한 줄 정리

이 프로젝트는 **Modbus TCP 기반으로 SCARA 계열 로봇을 테스트 제어하기 위한 WinForms 프로그램 + 공통 제어 라이브러리**로 볼 수 있습니다.

실무에서 쓰려면 **주소맵 정리, 안전 인터락, 완료 확인 로직, 예외 처리**를 먼저 보강하는 것이 좋습니다.

