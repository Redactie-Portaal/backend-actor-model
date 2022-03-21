import http from 'k6/http';
import { sleep } from 'k6';
export const options = {
    vus: 50,
    duration: '30s',
};
export default function () {
    const responses = http.batch([
        //['GET', 'https://localhost:7236/hello',  null ],
        //['GET', 'https://localhost:7236/bye',  null ],
        //['GET', 'https://localhost:7236/test',  null ]
        //['GET', 'https://localhost:7236/guid?greeting=gg',  null ]
        ['GET', 'https://localhost:7236/newsitem?guid=fd46b7d7-cbd0-46d3-bc85-69d8e694b242',  null ],
        ['GET', 'https://localhost:7236/newsitem?guid=745365a3-473f-4fa8-aec7-d3e7963580ac',  null ]

    ])
    //http.get('https://localhost:7236/hello');
    //http.get('https://localhost:7236/bye');
    //http.get('https://localhost:7236/test');
}
