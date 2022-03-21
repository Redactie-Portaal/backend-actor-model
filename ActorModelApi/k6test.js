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
        ['GET', 'https://localhost:7236/newsitem?guid=7dc3ec28-6fe6-44a5-b18b-43fa5f7412c0',  null ]
        //['GET', 'https://localhost:7236/newsitem?guid=7dc3ec28-6fe6-44a5-b18b-43fa5f7412c0',  null ]

    ])
    //http.get('https://localhost:7236/hello');
    //http.get('https://localhost:7236/bye');
    //http.get('https://localhost:7236/test');
}
