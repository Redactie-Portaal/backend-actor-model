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
        ['GET', 'https://localhost:7236/newsitem?guid=274fef19-f20f-472a-88a4-9384893c197f',  null ],
        ['GET', 'https://localhost:7236/newsitem?guid=10dd4ca5-8cae-4e54-882f-bb2760c38b78',  null ]

    ])
    //http.get('https://localhost:7236/hello');
    //http.get('https://localhost:7236/bye');
    //http.get('https://localhost:7236/test');
}
