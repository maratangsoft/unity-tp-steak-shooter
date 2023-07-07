# Unity Toy Project: The Steak Shooter
## Overview
달려오는 동물들에게 스테이크를 쏴서 맞추는 탑다운 슈팅 미니게임입니다.<br>
[Unity Learn - Junior Programmer Unit 2](https://learn.unity.com/project/2danweon-gibon-geimpeulrei) 과정에서 제작한 프로젝트에 여러 컨텐츠를 추가하여 만들었습니다.<br>
<br>
[Play Game](https://play.unity.com/mg/other/webgl-builds-357811)

## Objectives
- 화면 위에서 나타난 동물이 아래로 지나가기 전에 쏴맞추기
- 일정시간 버티기

## Number of Stages
5

## Stage Information
|스테이지|동물 종류|스폰 주기|
|--|--|--|
|1|3종|1.4초|
|2|3종|1.1초|
|3|4종|1.1초|
|4|4종|0.8초|
|5|5종|0.8초|

## Player Bonus Item
총알 파워업: 3갈래 발사

## UI
- 메인메뉴
  - 타이틀
  - 스타트 버튼
- 게임 중 메뉴
  - 스테이지명
  - 잔여시간 타이머
- 스테이지 클리어 메뉴
  - 스테이지 클리어 텍스트
  - 다음 스테이지 버튼
- 게임오버 메뉴
  - 게임오버 텍스트
  - 리트라이 버튼
- 올 클리어 메뉴
  - 축하 텍스트

## Optimization Skills
- 총알과 동물에 오브젝트풀 적용
- 화면 밖으로 나간 총알과 동물 객체 비활성화
- 플랫한 하이어라키 구조

## Sounds and FX (Not Implemented yet)
- 명중 파티클
- 발사 사운드
- 명중 사운드
- BGM

## Build
- WebGL
