#ifndef GLOBAL_SPHEREMASK_INCLUDED
#define GLOBAL_SPHEREMASK_INCLUDED

#include "Common.hlsl"
#include "Assets/INab Studio/Common/Subgraphs/SDFs/3D_SDFs.hlsl"

// Global keywords are used only for array properties
// The _MASKSNUMBER__ and _TYPE_ must be set per material
// As well as the _WORLDDISSOLVE_ID__

// ID 1 
//=============================================

float4 WorldDissolve_1_Positions[ArrayMax];
float4 WorldDissolve_1_Scales[ArrayMax];

// Rotation
float4 WorldDissolve_1_Forwards[ArrayMax];
float4 WorldDissolve_1_Rights[ArrayMax];
float4 WorldDissolve_1_Ups[ArrayMax];

// Solid Angles
float WorldDissolve_1_Angles[ArrayMax];
float WorldDissolve_1_Lengths[ArrayMax];

// Round Cones
float4 WorldDissolve_1_Positions_2[ArrayMax];

//=============================================

// ID 2
//=============================================

float4 WorldDissolve_2_Positions[ArrayMax];
float4 WorldDissolve_2_Scales[ArrayMax];

// Rotation
float4 WorldDissolve_2_Forwards[ArrayMax];
float4 WorldDissolve_2_Rights[ArrayMax];
float4 WorldDissolve_2_Ups[ArrayMax];

// Solid Angles
float WorldDissolve_2_Angles[ArrayMax];
float WorldDissolve_2_Lengths[ArrayMax];

// Round Cones
float4 WorldDissolve_2_Positions_2[ArrayMax];

//=============================================

// ID 3
//=============================================

float4 WorldDissolve_3_Positions[ArrayMax];
float4 WorldDissolve_3_Scales[ArrayMax];

// Rotation
float4 WorldDissolve_3_Forwards[ArrayMax];
float4 WorldDissolve_3_Rights[ArrayMax];
float4 WorldDissolve_3_Ups[ArrayMax];

// Solid Angles
float WorldDissolve_3_Angles[ArrayMax];
float WorldDissolve_3_Lengths[ArrayMax];

// Round Cones
float4 WorldDissolve_3_Positions_2[ArrayMax];

//=============================================

// ID 4
//=============================================

float4 WorldDissolve_4_Positions[ArrayMax];
float4 WorldDissolve_4_Scales[ArrayMax];

// Rotation
float4 WorldDissolve_4_Forwards[ArrayMax];
float4 WorldDissolve_4_Rights[ArrayMax];
float4 WorldDissolve_4_Ups[ArrayMax];

// Solid Angles
float WorldDissolve_4_Angles[ArrayMax];
float WorldDissolve_4_Lengths[ArrayMax];

// Round Cones
float4 WorldDissolve_4_Positions_2[ArrayMax];

//=============================================

void MasksGlobal_float(float3 worldSpacePosition, out float Out)
{
	float sum_1 = 0;
	float sum_2 = 0;
	float sum_3 = 0;
	float sum_4 = 0;

#if defined(_WORLDDISSOLVE_ID__1)
	sum_1 = CalculateSDFs(worldSpacePosition, WorldDissolve_1_Positions, WorldDissolve_1_Scales,
		WorldDissolve_1_Forwards, WorldDissolve_1_Rights, WorldDissolve_1_Ups,
		 WorldDissolve_1_Positions_2); 
#elif _WORLDDISSOLVE_ID__2
	sum_2 = CalculateSDFs(worldSpacePosition, WorldDissolve_2_Positions, WorldDissolve_2_Scales,
		WorldDissolve_2_Forwards, WorldDissolve_2_Rights, WorldDissolve_2_Ups,
		WorldDissolve_2_Positions_2);
#elif _WORLDDISSOLVE_ID__3
	sum_3 = CalculateSDFs(worldSpacePosition, WorldDissolve_3_Positions, WorldDissolve_3_Scales,
		WorldDissolve_3_Forwards, WorldDissolve_3_Rights, WorldDissolve_3_Ups,
		 WorldDissolve_3_Positions_2);
#elif _WORLDDISSOLVE_ID__4
	sum_4 = CalculateSDFs(worldSpacePosition, WorldDissolve_4_Positions, WorldDissolve_4_Scales,
		WorldDissolve_4_Forwards, WorldDissolve_4_Rights, WorldDissolve_4_Ups,
		WorldDissolve_4_Positions_2);
#endif

#if defined(_WORLDDISSOLVE_ID__0)
	Out = 1;
#else
	Out = sum_1 + sum_2 + sum_3 + sum_4;
#endif
}

#endif