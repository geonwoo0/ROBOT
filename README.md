# Robot Modbus Control System

본 프로젝트는 Modbus TCP 프로토콜을 기반으로 로봇 암(Robot Arm)을 제어하고 상태를 모니터링하기 위한 C# 솔루션입니다. 
티칭 펜던트 없이 PC 환경에서 로봇의 관절 이동(MoveJ), 선형 이동(MoveL), 조그(Jog) 제어 및 다중 웨이포인트 기반의 순차 제어(Sequence Control)를 수행할 수 있도록 설계되었습니다.

<img width="807" height="488" alt="image" src="https://github.com/user-attachments/assets/df86400b-270c-4921-a37c-7fdc1f516940" />

## 🚀 핵심 기능 (Key Features)

* **실시간 모니터링 (Real-time Polling)**: 로봇의 현재 Cartesian(X, Y, Z, R) 및 관절(J1~J4) 좌표를 실시간으로 읽어옵니다.
* **통합 모션 제어 (Motion Control)**:
  * `MoveJ` (Joint Motion): 각 축의 관절 각도를 지정하여 이동합니다.
  * `MoveL` (Linear Motion): 직교 좌표계 기반으로 툴 끝단을 선형으로 이동합니다.
* **조그 제어 (Jogging)**: J1~J4 축에 대한 개별 수동 조작(Jog Up/Down)을 지원합니다.
* **웨이포인트 순차 구동 (Waypoint Sequencing)**: 
  * 목표 지점들을 리스트에 추가하고 순차적으로 실행할 수 있습니다.
  * 로봇의 Moving Flag를 감시하여 한 동작이 완료된 후 다음 동작을 수행하도록 동기화되어 있습니다.
* **JSON 상태 저장/불러오기**: 교시(Teaching)한 웨이포인트 데이터를 `.json` 파일로 저장하고 로드할 수 있습니다.

## 🏗 시스템 아키텍처 및 모듈 구성 (Architecture)

솔루션은 크게 코어 통신 계층, 모션 실행 계층, 그리고 UI 테스트 계층으로 분리되어 있습니다.

* **`RobotCore`**
  * `ModbusHelper.cs`: `NModbus` 라이브러리를 래핑하여 TCP 연결을 관리합니다. SD, SM, D, M 등의 PLC 주소 체계를 Modbus Address Offset으로 자동 변환합니다.
  * `RcPointCodec.cs`: 로봇 컨트롤러가 요구하는 `RCPATH` 포인트 패킷(속도, 가감속, 좌표 등) 구조체를 100 워드(Word) 크기의 ushort 배열로 직렬화(Serialization)합니다.
* **`RobotMoveJ` & `RobotMoveL`**
  * `MoveJExecutor.cs` / `MoveLExecutor.cs`: Cartesian 좌표계와 Joint 좌표계를 구분하여 `RcPointInformation` 파라미터를 구성하고, 핸드셰이크 레지스터를 통해 제어기에 모션 실행 명령을 하달합니다.
* **`RobotTest`**
  * `Form1.cs`: 사용자 인터페이스(WinForms)를 제공합니다. 백그라운드 태스크를 통해 폴링(Polling) 루프를 돌며 상태를 갱신하고 제어 명령을 비동기로 처리합니다.

## 🔌 주요 Modbus 레지스터 맵 (Register Mapping)

내부적으로 사용되는 주요 메모리 주소 할당표입니다. 제어기와의 인터페이스 시 핵심적인 역할을 수행합니다.

### 1. 상태 모니터링 (Read)
| 메모리 주소 | 타입 | 설명 |
| :--- | :--- | :--- |
| `SD4000` ~ `SD4006` | Float (32-bit) | 현재 직교 좌표 (X, Y, Z, R) |
| `SD4020` ~ `SD4026` | Float (32-bit) | 현재 관절 각도 (J1, J2, J3, J4) |
| `SM3001` | Coil (1-bit) | 로봇 이동 상태 (Moving Flag, true: 이동 중) |

### 2. 모션 제어 (Write)
| 메모리 주소 | 타입 | 설명 |
| :--- | :--- | :--- |
| `D1000` ~ `D1099` | Holding Register | 목표 모션 파라미터 블록 (100 words, RCPATH 구조체) |
| `D100` | Holding Register | 명령 트리거 핸드셰이크 (1 기록) |
| `M10` | Coil (1-bit) | 모션 준비 플래그 |
| `SM3160` | Coil (1-bit) | 모션 실행(Execute) 펄스 코일 |

### 3. 시스템 및 조그 제어 (System & Jog)
| 메모리 주소 | 타입 | 설명 |
| :--- | :--- | :--- |
| `M0` | Coil (1-bit) | 서보 ON / OFF |
| `M9998` | Coil (1-bit) | 로봇 정지 (STOP) |
| `M9999` | Coil (1-bit) | 비상 정지 (E-STOP) |
| `SM3004`, `SM3005` | Coil (1-bit) | 에러 리셋 코일 |
| `M100` ~ `M107` | Coil (1-bit) | J1~J4 축 Jog Up/Down 동작 (버튼 누름 유지 시 동작) |

## ⚙️ 시작 가이드 (Getting Started)

### 사전 요구 사항 (Prerequisites)
* .NET Framework 4.7.2 이상
* `NModbus` 패키지 참조
* 로봇 컨트롤러 IP 설정: 기본값 `192.168.31.101`, Port `502` (`Form1.cs`의 `InitModbus()`에서 수정 가능)

### 사용 방법 (Usage)
1. **연결 및 서보 구동**: 프로그램을 실행하면 자동으로 로봇과 Modbus 연결을 시도합니다. 연결 성공 후 `RBON` 버튼을 클릭하여 서보 전원을 투입합니다.
2. **수동 조작 (Jogging)**: `J1+` ~ `J4-` 버튼을 마우스로 누르고 있는 동안 로봇이 해당 축으로 이동합니다.
3. **포인트 교시 및 리스트 실행**:
   * 조그를 통해 원하는 위치로 이동 후 `ADD` 버튼을 눌러 현재 위치를 리스트에 추가합니다.
   * `Save JSON`을 통해 교시한 웨이포인트(`waypoints.json`)를 저장할 수 있습니다.
   * `Run List` 버튼을 클릭하면 리스트 내의 모든 포인트를 J1~J4 관절 각도를 기준으로 순차 이동(MoveJ)합니다.

## ⚠️ 안전 주의 사항 (Safety Precautions)
본 애플리케이션을 통해 산업용 로봇을 제어할 때는 물리적 충돌에 유의해야 합니다. 테스트 전 속도(Speed)와 가감속(Acc, Dec) 파라미터를 낮게(예: 10% 이하) 설정하고, 문제 발생 시 즉시 `ESTOP` 버튼 또는 하드웨어 비상 정지 스위치를 누를 수 있도록 준비해 주십시오.
