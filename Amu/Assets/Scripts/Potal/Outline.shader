//Portal 시스템의 외곽선을 그리기 위한 URP 쉐이더
Shader "Portal/Outline"
{
    Properties
    {
        //외곽선 색상을 설정하는 프로퍼티, Inspector에서 조절 가능
        _OutlineColour("Outline Colour", Color) = (1,1,1,1)

        //스텐실 버퍼에서 사용할 마스크 ID
        _MaskID("Mask ID", Int) = 1
    }
    SubShader
    {
        Tags
        { 
            "RenderType"="Opaque"                       //불투명 렌더링
            "Queue" = "Geometry+2"                      //일반 지오메트리 후에 렌더링
            "RenderPipeline" = "UniversalPipeline"      //URP 파이프 라인 사용
        }

        HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        ENDHLSL

        //스텐실 버퍼 설정 - 포털 내부와 외곽선이 겹치지 않도록 함
    }
    FallBack "Diffuse"
}
