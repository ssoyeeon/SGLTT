//Portal �ý����� �ܰ����� �׸��� ���� URP ���̴�
Shader "Portal/Outline"
{
    Properties
    {
        //�ܰ��� ������ �����ϴ� ������Ƽ, Inspector���� ���� ����
        _OutlineColour("Outline Colour", Color) = (1,1,1,1)

        //���ٽ� ���ۿ��� ����� ����ũ ID
        _MaskID("Mask ID", Int) = 1
    }
    SubShader
    {
        Tags
        { 
            "RenderType"="Opaque"                       //������ ������
            "Queue" = "Geometry+2"                      //�Ϲ� ������Ʈ�� �Ŀ� ������
            "RenderPipeline" = "UniversalPipeline"      //URP ������ ���� ���
        }

        HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        ENDHLSL

        //���ٽ� ���� ���� - ���� ���ο� �ܰ����� ��ġ�� �ʵ��� ��
    }
    FallBack "Diffuse"
}
