import { styled } from "styled-components";

export const AppPreviewContainer = styled.div`
    width: 100%;
    position: relative;
    #anti-spy-alert{
        position: absolute !important;
    }
    .wds_1_183_0_CardContent__root{
        padding: 17px 25px;
    }
    .anti-spy-preview-img{
        width: 100%;
        padding-bottom: 100%;
        position: relative;
        img{
            top: 0;
            left: 0;
            position: absolute;
            width: 100%;
            height: 100%;
            object-fit: cover;
            z-index: 1;
        }
        .anti-spy-preview-tag{
            top: 15px;
            left: 15px;
            position: absolute;
            z-index: 2;
            background: #7247FF;
            padding: 4px 8px;
            display: flex;
            align-items: center;
            span{
                font-weight: 300;
                color: #ffffff;
            }
        }
    }
`