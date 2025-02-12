Shader "Custom/InvisibleShader"
{
    SubShader
    {
        // Тег RenderType можно настроить по необходимости
        Tags { "RenderType"="Opaque" }
        Pass
        {
            // ColorMask 0 отключает запись цвета, таким образом объект не будет виден
            ColorMask 0

            // При необходимости можно отключить и запись глубины:
            // ZWrite Off
        }
    }
}