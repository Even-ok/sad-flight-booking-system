
let bus = new Vue()
Vue.prototype.bus = bus

    var app = new Vue({
        el: '#searchbox',
        data() {
            return {
                value: '',
                options: [{
                    value: 'Anhui',
                    label: '安徽省',
                    children: [{
                        value: 'anqing',
                        label: '安庆',
                        children: [{
                            value: 'AQG',
                            label: '大龙山机场'
                        }]
                    }, {
                        value: 'bengbu',
                        label: '蚌埠',
                        children: [{
                            value: 'BFU',
                            label: '蚌埠机场'
                        }]
                    },
                    {
                        value: 'fuyang',
                        label: '阜阳',
                        children: [{
                            value: 'FUG',
                            label: '西关机场'
                        }]
                    },
                    {
                        value: 'hefei',
                        label: '合肥',
                        children: [{
                            value: 'HFE',
                            label: '骆岗机场'
                        }]
                    },
                    {
                        value: 'huangshan',
                        label: '黄山',
                        children: [{
                            value: 'TXN',
                            label: '屯溪机场'
                        }]
                    }]
                }, {
                    value: 'beijing',
                    label: '北京',
                    children: [{
                        value: 'NAY',
                        label: '南苑机场'
                    }, {
                        value: 'PEK',
                        label: '首都国际机场'
                        }, {
                            value: 'PKX',
                            label: '北京大兴机场'
                        }]
                }, {
                    value: 'fujian',
                    label: '福建省',
                    children: [{
                        value: 'fuzhou',
                        label: '福州',
                        children: [{
                            value: 'FOC',
                            label: '长乐国际机场'
                        }]
                    }, {
                        value: 'jinjiang',
                        label: '晋江',
                        children: [{
                            value: 'JJN',
                            label: '晋江机场'
                        }]
                    }, {
                        value: 'liancheng',
                        label: '连城',
                        children: [{
                            value: 'LCX',
                            label: '连城机场'
                        }]
                    }, {
                        value: 'wuyishan',
                        label: '武夷山',
                        children: [{
                            value: 'WUS',
                            label: '武夷山机场'
                        }]
                    }, {
                        value: 'xiamen',
                        label: '厦门',
                        children: [{
                            value: 'XMN',
                            label: '高崎国际机场'
                        }]
                    }]
                },
                {
                    value: 'gansu',
                    label: '甘肃省',
                    children: [{
                        value: 'jiuquan',
                        label: '酒泉',
                        children: [{
                            value: 'CHW',
                            label: '酒泉机场'
                        }]
                    }, {
                        value: 'dunhuang',
                        label: '敦煌',
                        children: [{
                            value: 'DNH',
                            label: '敦煌机场'
                        }]
                    }, {
                        value: 'qingyang',
                        label: '庆阳',
                        children: [{
                            value: 'IQN',
                            label: '西峰镇机场'
                        }]
                    }, {
                        value: 'jiayuguan',
                        label: '嘉峪关',
                        children: [{
                            value: 'JGN',
                            label: '嘉峪关机场'
                        }]
                    }, {
                        value: 'lanzhou',
                        label: '兰州',
                        children: [{
                            value: 'LHW',
                            label: '中川机场'
                        }]
                    }]
                },
                {
                    value: 'gangdong',
                    label: '广东省',
                    children: [{
                        value: 'guangzhou',
                        label: '广州',
                        children: [{
                            value: 'CAN',
                            label: '白云国际机场'
                        }]
                    }, {
                        value: 'foshan',
                        label: '佛山',
                        children: [{
                            value: 'FUO',
                            label: '沙堤机场'
                        }]
                    }, {
                        value: 'meixian',
                        label: '梅县',
                        children: [{
                            value: 'MXZ',
                            label: '梅县机场'
                        }]
                    }, {
                        value: 'shantou',
                        label: '汕头',
                        children: [{
                            value: 'SWA',
                            label: '外砂机场'
                        }]
                    }, {
                        value: 'shenzhen',
                        label: '深圳',
                        children: [{
                            value: 'SZX',
                            label: '宝安国际机场'
                        }]
                    }, {
                        value: 'zhanjiang',
                        label: '湛江',
                        children: [{
                            value: 'ZHA',
                            label: '湛江机场'
                        }]
                    }, {
                        value: 'zhuhai',
                        label: '珠海',
                        children: [{
                            value: 'ZUH',
                            label: '三灶机场'
                        }]
                    }]
                },
                {
                    value: 'gangxi',
                    label: '广西省',
                    children: [{
                        value: 'beihai',
                        label: '北海',
                        children: [{
                            value: 'BHY',
                            label: '福城机场'
                        }]
                    }, {
                        value: 'guilin',
                        label: '桂林',
                        children: [{
                            value: 'KWL',
                            label: '两江国际机场'
                        }]
                    }, {
                        value: 'liuzhou',
                        label: '柳州',
                        children: [{
                            value: 'LZH',
                            label: '白莲机场'
                        }]
                    }, {
                        value: 'nanning',
                        label: '南宁',
                        children: [{
                            value: 'NNG',
                            label: '吴墟机场'
                        }]
                    }, {
                        value: 'wuzhou',
                        label: '梧州',
                        children: [{
                            value: 'WUZ',
                            label: '长州岛机场'
                        }]
                    }]
                },
                {
                    value: 'guizhou',
                    label: '贵州省',
                    children: [{
                        value: 'xingyi',
                        label: '兴义',
                        children: [{
                            value: 'ACX',
                            label: '兴义机场'
                        }]
                    }, {
                        value: 'baise',
                        label: '百色',
                        children: [{
                            value: 'AEB',
                            label: '田阳机场'
                        }]
                    }, {
                        value: 'guiyang',
                        label: '贵阳',
                        children: [{
                            value: 'KWE',
                            label: '龙洞堡机场'
                        }]
                    }, {
                        value: 'tongren',
                        label: '铜仁',
                        children: [{
                            value: 'TEN',
                            label: '大兴机场'
                        }]
                    }, {
                        value: 'zunyi',
                        label: '遵义',
                        children: [{
                            value: 'ZYI',
                            label: '遵义机场'
                        }]
                    }, {
                        value: 'liping',
                        label: '黎平',
                        children: [{
                            value: 'HZH',
                            label: '黎平机场'
                        }]
                    }, {
                        value: 'anshun',
                        label: '安顺',
                        children: [{
                            value: 'AVA',
                            label: '黄果树机场'
                        }]
                    }]
                },
                {
                    value: 'hainan',
                    label: '海南省',
                    children: [{
                        value: 'haikou',
                        label: '海口',
                        children: [{
                            value: 'HAK',
                            label: '美兰国际机场'
                        }]
                    }, {
                        value: 'sanya',
                        label: '三亚',
                        children: [{
                            value: 'SYX',
                            label: '凤凰国际机场'
                        }]
                    }]
                },
                {
                    value: 'hebei',
                    label: '河北省',
                    children: [{
                        value: 'handan',
                        label: '邯郸',
                        children: [{
                            value: 'HDG',
                            label: '邯郸机场'
                        }]
                    }, {
                        value: 'qinhuangdao',
                        label: '秦皇岛',
                        children: [{
                            value: 'SHP',
                            label: '山海关机场'
                        }]
                    }, {
                        value: 'shijiazhuang',
                        label: '石家庄',
                        children: [{
                            value: 'SJW',
                            label: '正定机场'
                        }]
                    }]
                },
                {
                    value: 'henan',
                    label: '河南省',
                    children: [{
                        value: 'anyang',
                        label: '安阳',
                        children: [{
                            value: 'AYN',
                            label: '安阳机场'
                        }]
                    }, {
                        value: 'zhengzhou',
                        label: '郑州',
                        children: [{
                            value: 'CGO',
                            label: '新郑国际机场'
                        }]
                    }, {
                        value: 'luoyang',
                        label: '洛阳',
                        children: [{
                            value: 'LYA',
                            label: '北郊机场'
                        }]
                    }, {
                        value: 'nanyang',
                        label: '南阳',
                        children: [{
                            value: 'NNY',
                            label: '姜营机场'
                        }]
                    }]
                },
                {
                    value: 'heilongjiang',
                    label: '黑龙江省',
                    children: [{
                        value: 'daqing',
                        label: '大庆',
                        children: [{
                            value: 'DQA',
                            label: '萨尔图机场'
                        }]
                    }, {
                        value: 'heihe',
                        label: '黑河',
                        children: [{
                            value: 'HEK',
                            label: '黑河机场'
                        }]
                    }, {
                        value: 'haerbin',
                        label: '哈尔滨',
                        children: [{
                            value: 'HRB',
                            label: '太平国际机场'
                        }]
                    }, {
                        value: 'jiamusi',
                        label: '佳木斯',
                        children: [{
                            value: 'JMU',
                            label: '东郊机场'
                        }]
                    }, {
                        value: 'mudanjiang',
                        label: '牡丹江',
                        children: [{
                            value: 'MDG',
                            label: '海浪机场'
                        }]
                    }, {
                        value: 'qiqihaer',
                        label: '齐齐哈尔',
                        children: [{
                            value: 'NDG',
                            label: '三家子机场'
                        }]
                    }]
                },
                {
                    value: 'hubei',
                    label: '湖北省',
                    children: [{
                        value: 'enshi',
                        label: '恩施',
                        children: [{
                            value: 'ENH',
                            label: '许家坪机场'
                        }]
                    }, {
                        value: 'xiangfan',
                        label: '襄樊',
                        children: [{
                            value: 'XFN',
                            label: '刘集机场'
                        }]
                    }, {
                        value: 'wuhan',
                        label: '武汉',
                        children: [{
                            value: 'WUH',
                            label: '天河国际机场'
                        }]
                    }, {
                        value: 'yichang',
                        label: '宜昌',
                        children: [{
                            value: 'YIH',
                            label: '三峡机场'
                        }]
                    }]
                },
                {
                    value: 'hunan',
                    label: '湖南省',
                    children: [{
                        value: 'changde',
                        label: '常德',
                        children: [{
                            value: 'CGD',
                            label: '桃花机场'
                        }]
                    }, {
                        value: 'changsha',
                        label: '长沙',
                        children: [{
                            value: 'CSX',
                            label: '黄花国际机场'
                        }]
                    }, {
                        value: 'zhangjiajie',
                        label: '张家界',
                        children: [{
                            value: 'DYG',
                            label: '荷花机场'
                        }]
                    }, {
                        value: 'zhijiang',
                        label: '芷江',
                        children: [{
                            value: 'HJJ',
                            label: '芷江机场'
                        }]
                    }, {
                        value: 'yongzhou',
                        label: '永州',
                        children: [{
                            value: 'LLF',
                            label: '零陵机场'
                        }]
                    }]
                },
                {
                    value: 'jilin',
                    label: '吉林省',
                    children: [{
                        value: 'changchun',
                        label: '长春',
                        children: [{
                            value: 'CGQ',
                            label: '龙嘉国际机场'
                        }]
                    }, {
                        value: 'jilin',
                        label: '吉林',
                        children: [{
                            value: 'JIL',
                            label: '二台子机场'
                        }]
                    }, {
                        value: 'tonghua',
                        label: '通化',
                        children: [{
                            value: 'TNH',
                            label: '通化机场'
                        }]
                    }]
                },
                {
                    value: 'jiangsu',
                    label: '江苏省',
                    children: [{
                        value: 'changzhou',
                        label: '常州',
                        children: [{
                            value: 'CZX',
                            label: '奔牛机场'
                        }]
                    }, {
                        value: 'lianyungang',
                        label: '连云港',
                        children: [{
                            value: 'LYG',
                            label: '白塔埠机场'
                        }]
                    }, {
                        value: 'nanjing',
                        label: '南京',
                        children: [{
                            value: 'NKG',
                            label: '禄口国际机场'
                        }]
                    }, {
                        value: 'nantong',
                        label: '南通',
                        children: [{
                            value: 'NTG',
                            label: '兴东机场'
                        }]
                    }, {
                        value: 'wuxi',
                        label: '无锡',
                        children: [{
                            value: 'WUX',
                            label: '无锡机场'
                        }]
                    }, {
                        value: 'xuzhou',
                        label: '徐州',
                        children: [{
                            value: 'XUZ',
                            label: '观音机场'
                        }]
                    }, {
                        value: 'huaian',
                        label: '淮安',
                        children: [{
                            value: 'HIA',
                            label: '涟水机场'
                        }]
                    }, {
                        value: 'yancheng',
                        label: '盐城',
                        children: [{
                            value: 'YNZ',
                            label: '南洋机场'
                        }]
                    }]
                },
                {
                    value: '江西',
                    label: '江西省',
                    children: [{
                        value: 'jingdezhen',
                        label: '景德镇',
                        children: [{
                            value: 'JDZ',
                            label: '罗家机场'
                        }]
                    }, {
                        value: 'jinggangshan',
                        label: '井冈山',
                        children: [{
                            value: 'JGS',
                            label: '井冈山机场'
                        }]
                    }, {
                        value: 'jiujiang',
                        label: '九江',
                        children: [{
                            value: 'JIU',
                            label: '庐山机场'
                        }]
                    }, {
                        value: 'nanchang',
                        label: '南昌',
                        children: [{
                            value: 'KHN',
                            label: '昌北机场'
                        }]
                    }, {
                        value: 'ganzhou',
                        label: '赣州',
                        children: [{
                            value: 'KOW',
                            label: '黄金机场'
                        }]
                    }]
                },
                {
                    value: 'liaoning',
                    label: '辽宁省',
                    children: [{
                        value: 'anshan',
                        label: '鞍山',
                        children: [{
                            value: 'AOG',
                            label: '腾鳌机场'
                        }]
                    }, {
                        value: 'chaoyang',
                        label: '朝阳',
                        children: [{
                            value: 'CHG',
                            label: '朝阳机场'
                        }]
                    }, {
                        value: 'changhai',
                        label: '长海',
                        children: [{
                            value: 'CNI',
                            label: '大长山岛机场'
                        }]
                    }, {
                        value: 'dandong',
                        label: '丹东',
                        children: [{
                            value: 'DDG',
                            label: '浪头机场'
                        }]
                    }, {
                        value: 'dalian',
                        label: '大连',
                        children: [{
                            value: 'DLC',
                            label: '周水子机场'
                        }]
                    }, {
                        value: 'changhai',
                        label: '锦州',
                        children: [{
                            value: 'JNZ',
                            label: '小岭子机场'
                        }]
                    }, {
                        value: 'shenyang',
                        label: '沈阳',
                        children: [{
                            value: 'SHE',
                            label: '桃仙机场'
                        }]
                    }, {
                        value: 'yanji',
                        label: '延吉',
                        children: [{
                            value: 'YNJ',
                            label: '朝阳川机场'
                        }]
                    }]
                },
                {
                    value: 'neimenggu',
                    label: '内蒙古',
                    children: [{
                        value: 'baotou',
                        label: '包头',
                        children: [{
                            value: 'BAV',
                            label: '海兰泡机场'
                        }]
                    }, {
                        value: 'chifeng',
                        label: '赤峰',
                        children: [{
                            value: 'CIF',
                            label: '土城子机场'
                        }]
                    }, {
                        value: 'eerduosi',
                        label: '鄂尔多斯',
                        children: [{
                            value: 'DSN',
                            label: '鄂尔多斯机场'
                        }]
                    }, {
                        value: 'huhehaote',
                        label: '呼和浩特',
                        children: [{
                            value: 'HET',
                            label: '白塔国际机场'
                        }]
                    }, {
                        value: 'hailaer',
                        label: '海拉尔',
                        children: [{
                            value: 'HLD',
                            label: '东山机场'
                        }]
                    }, {
                        value: 'wulanhaote',
                        label: '乌兰浩特',
                        children: [{
                            value: 'HLH',
                            label: '乌兰浩特机场'
                        }]
                    }, {
                        value: 'manzhouli',
                        label: '满洲里',
                        children: [{
                            value: 'NZH',
                            label: '西郊机场'
                        }]
                    }, {
                        value: 'tongliao',
                        label: '通辽',
                        children: [{
                            value: 'TGO',
                            label: '通辽机场'
                        }]
                    }, {
                        value: 'wuhai',
                        label: '乌海',
                        children: [{
                            value: 'WUA',
                            label: '乌海机场'
                        }]
                    }, {
                        value: 'xilinhaote',
                        label: '锡林浩特',
                        children: [{
                            value: 'XIL',
                            label: '锡林浩特机场'
                        }]
                    }]
                },
                {
                    value: 'ningxia',
                    label: '宁夏',
                    children: [{
                        value: 'yinchuan',
                        label: '银川',
                        children: [{
                            value: 'INC',
                            label: '河东机场'
                        }]
                    }]
                },
                {
                    value: 'qinghai',
                    label: '青海省',
                    children: [{
                        value: 'geermu',
                        label: '格尔木',
                        children: [{
                            value: 'GOQ',
                            label: '格尔木机场'
                        }]
                    }, {
                        value: 'xining',
                        label: '西宁',
                        children: [{
                            value: 'XNN',
                            label: '曹家堡机场'
                        }]
                    }]
                },
                {
                    value: 'shandong',
                    label: '山东省',
                    children: [{
                        value: 'dongying',
                        label: '东营',
                        children: [{
                            value: 'DOY',
                            label: '永安机场'
                        }]
                    }, {
                        value: 'jining',
                        label: '济宁',
                        children: [{
                            value: 'JNG',
                            label: '曲阜机场'
                        }]
                    }, {
                        value: 'qingdao',
                        label: '青岛',
                        children: [{
                            value: 'TAO',
                            label: '流亭国际机场'
                        }]
                    }, {
                        value: 'jinan',
                        label: '济南',
                        children: [{
                            value: 'TNA',
                            label: '遥墙国际机场'
                        }]
                    }, {
                        value: 'weifang',
                        label: '潍坊',
                        children: [{
                            value: 'WEF',
                            label: '文登机场'
                        }]
                    }, {
                        value: 'weihai',
                        label: '威海',
                        children: [{
                            value: 'WEH',
                            label: '大水泊机场'
                        }]
                    }, {
                        value: 'yantai',
                        label: '烟台',
                        children: [{
                            value: 'YNT',
                            label: '莱山机场'
                        }]
                    }]
                },
                {
                    value: 'shanxi',
                    label: '山西省',
                    children: [{
                        value: 'changzhi',
                        label: '长治',
                        children: [{
                            value: 'CIH',
                            label: '王村机场'
                        }]
                    }, {
                        value: 'datong',
                        label: '大同',
                        children: [{
                            value: 'DAT',
                            label: '怀仁机场'
                        }]
                    }, {
                        value: 'linyi',
                        label: '临沂',
                        children: [{
                            value: 'LYI',
                            label: '临沂机场'
                        }]
                    }, {
                        value: 'taiyuan',
                        label: '太原',
                        children: [{
                            value: 'TYN',
                            label: '武宿机场'
                        }]
                    }, {
                        value: 'yuncheng',
                        label: '运城',
                        children: [{
                            value: 'YCU',
                            label: '关公机场'
                        }]
                    }]
                },
                {
                    value: 'shaanxi',
                    label: '陕西省',
                    children: [{
                        value: 'ankang',
                        label: '安康',
                        children: [{
                            value: 'AKA',
                            label: '五里铺机场'
                        }]
                    }, {
                        value: 'yanan',
                        label: '延安',
                        children: [{
                            value: 'ENY',
                            label: '二十里铺机场'
                        }]
                    }, {
                        value: 'hanzhong',
                        label: '汉中',
                        children: [{
                            value: 'HZG',
                            label: '西关机场'
                        }]
                    }, {
                        value: 'yulin',
                        label: '榆林',
                        children: [{
                            value: 'UYN',
                            label: '西沙机场'
                        }]
                    }, {
                        value: 'xian',
                        label: '西安',
                        children: [{
                            value: 'XIY',
                            label: '咸阳国际机场'
                        }]
                    }]
                },
                {
                    value: 'shanghai',
                    label: '上海',
                    children: [{
                        value: 'PVG',
                        label: '浦东国际机场'
                    }, {
                        value: 'SHA',
                        label: '虹桥国际机场'
                    }]
                },
                {
                    value: 'sichuan',
                    label: '四川省',
                    children: [{
                        value: 'chengdu',
                        label: '成都',
                        children: [{
                            value: 'TFU',
                            label: '天府机场'
                        }]
                    }, {
                        value: 'daxian',
                        label: '达县',
                        children: [{
                            value: 'DAX',
                            label: '河市霸机场'
                        }]
                    }, {
                        value: 'guangyuan',
                        label: '广元',
                        children: [{
                            value: 'GNY',
                            label: '广元机场'
                        }]
                    }, {
                        value: 'jiuzhaigou',
                        label: '九寨沟',
                        children: [{
                            value: 'JZH',
                            label: '黄龙机场'
                        }]
                    }, {
                        value: 'luzhou',
                        label: '泸州',
                        children: [{
                            value: 'LZO',
                            label: '萱田机场'
                        }]
                    }, {
                        value: 'mianyang',
                        label: '绵阳',
                        children: [{
                            value: 'MIG',
                            label: '南郊机场'
                        }]
                    }, {
                        value: 'nanchong',
                        label: '南充',
                        children: [{
                            value: 'NAO',
                            label: '都尉坝机场'
                        }]
                    }, {
                        value: 'panzhihua',
                        label: '攀枝花',
                        children: [{
                            value: 'GNY',
                            label: '攀枝花机场'
                        }]
                    }, {
                        value: 'wanzhou',
                        label: '万州',
                        children: [{
                            value: 'WXN',
                            label: '万县机场'
                        }]
                    }, {
                        value: 'xichang',
                        label: '西昌',
                        children: [{
                            value: 'XIC',
                            label: '青山机场'
                        }]
                    }, {
                        value: 'yibin',
                        label: '宜宾',
                        children: [{
                            value: 'YBP',
                            label: '菜坝机场'
                        }]
                    }]
                },
                {
                    value: 'tianjin',
                    label: '天津',
                    children: [{
                        value: 'TSN',
                        label: '滨海国际机场'
                    }]
                },
                {
                    value: 'xinjiang',
                    label: '新疆',
                    children: [{
                        value: 'akesu',
                        label: '阿克苏',
                        children: [{
                            value: 'AKU',
                            label: '温宿机场'
                        }]
                    }, {
                        value: 'fuyun',
                        label: '富蕴',
                        children: [{
                            value: 'FYN',
                            label: '可可托托海机场'
                        }]
                    }, {
                        value: 'hami',
                        label: '哈密',
                        children: [{
                            value: 'HMI',
                            label: '哈密机场'
                        }]
                    }, {
                        value: 'hetian',
                        label: '和田',
                        children: [{
                            value: 'HTN',
                            label: '和田机场'
                        }]
                    }, {
                        value: 'qiemo',
                        label: '且末',
                        children: [{
                            value: 'IQM',
                            label: '且末机场'
                        }]
                    }, {
                        value: 'kuche',
                        label: '库车',
                        children: [{
                            value: 'KCA',
                            label: '库车机场'
                        }]
                    }, {
                        value: 'kashi',
                        label: '喀什',
                        children: [{
                            value: 'KHG',
                            label: '喀什机场'
                        }]
                    }, {
                        value: 'kuerle',
                        label: '库尔勒',
                        children: [{
                            value: 'KRL',
                            label: '库尔勒机场'
                        }]
                    }, {
                        value: 'kelamayi',
                        label: '克拉玛依',
                        children: [{
                            value: 'KRY',
                            label: '克拉玛依机场'
                        }]
                    }, {
                        value: 'tacheng',
                        label: '塔城',
                        children: [{
                            value: 'TCG',
                            label: '塔城机场'
                        }]
                    }, {
                        value: 'wulumuqi',
                        label: '乌鲁木齐',
                        children: [{
                            value: 'URC',
                            label: '地窝堡国际机场'
                        }]
                    }, {
                        value: 'yining',
                        label: '伊宁',
                        children: [{
                            value: 'YIN',
                            label: '伊宁机场'
                        }]
                    }]
                }, {
                    value: 'tibet',
                    label: '西藏',
                    children: [{
                        value: 'changdu',
                        label: '昌都',
                        children: [{
                            value: 'BPX',
                            label: '昌都马草机场'
                        }]
                    }, {
                        value: 'lasa',
                        label: '拉萨',
                        children: [{
                            value: 'LXA',
                            label: '贡嘎机场'
                        }]
                    }]
                },
                {
                    value: 'yunnan',
                    label: '云南省',
                    children: [{
                        value: 'baoshan',
                        label: '保山',
                        children: [{
                            value: 'BSD',
                            label: '保山机场'
                        }]
                    }, {
                        value: 'diqingxianggelila',
                        label: '迪庆香格里拉',
                        children: [{
                            value: 'DIG',
                            label: '迪庆机场'
                        }]
                    }, {
                        value: 'dali',
                        label: '大理',
                        children: [{
                            value: 'DLU',
                            label: '大理机场'
                        }]
                    }, {
                        value: 'xishuangbanna',
                        label: '西双版纳',
                        children: [{
                            value: 'JHG',
                            label: '景洪机场'
                        }]
                    }, {
                        value: 'kunming',
                        label: '昆明',
                        children: [{
                            value: 'SYX',
                            label: '机场'
                        }]
                    }, {
                        value: 'lijiang',
                        label: '丽江',
                        children: [{
                            value: 'LJG',
                            label: '丽江机场'
                        }]
                    }, {
                        value: 'lincang',
                        label: '临沧',
                        children: [{
                            value: 'LNJ',
                            label: '临沧机场'
                        }]
                    }, {
                        value: 'zhaotong',
                        label: '昭通',
                        children: [{
                            value: 'ZAT',
                            label: '昭通机场'
                        }]
                    }, {
                        value: 'dehongmangshi',
                        label: '德宏芒市',
                        children: [{
                            value: 'LUM',
                            label: '芒市机场'
                        }]
                    }, {
                        value: 'simao',
                        label: '思茅',
                        children: [{
                            value: 'SYM',
                            label: '思茅机场'
                        }]
                    }]
                },
                {
                    value: 'zhejiang',
                    label: '浙江省',
                    children: [{
                        value: 'hangzhou',
                        label: '杭州',
                        children: [{
                            value: 'HGH',
                            label: '萧山国际机场'
                        }]
                    }, {
                        value: 'zhoushan',
                        label: '舟山',
                        children: [{
                            value: 'HSN',
                            label: '普陀山机场'
                        }]
                    }, {
                        value: 'taizhou',
                        label: '台州',
                        children: [{
                            value: 'HYN',
                            label: '路桥机场'
                        }]
                    }, {
                        value: 'quzhou',
                        label: '衢州',
                        children: [{
                            value: 'JUZ',
                            label: '衢州机场'
                        }]
                    }, {
                        value: 'ningbo',
                        label: '宁波',
                        children: [{
                            value: 'NGB',
                            label: '栎社机场'
                        }]
                    }, {
                        value: 'wenzhou',
                        label: '温州',
                        children: [{
                            value: 'WNZ',
                            label: '永强机场'
                        }]
                    }, {
                        value: 'yiwu',
                        label: '义乌',
                        children: [{
                            value: 'YIW',
                            label: '义乌机场'
                        }]
                    }]
                },
                {
                    value: 'chongqing',
                    label: '重庆',
                    children: [{
                        value: 'CKG',
                        label: '江北国际机场'
                    }]
                },
                {
                    value: 'hongkong',
                    label: '香港',
                    children: [{
                        value: 'HKG',
                        label: '香港国际机场'
                    }]
                },
                {
                    value: 'macao',
                    label: '澳门',
                    children: [{
                        value: 'MFM',
                        label: '澳门国际机场'
                    }]
                }]
            }
        },
        created() {
            this.getDate()
        },
        methods: {
            getDate: function () {
                var date = new Date()
                var nowMonth = date.getMonth() + 1
                var strDate = date.getDate()
                var seperator = '-'
                if (nowMonth >= 1 && nowMonth <= 9) {
                    nowMonth = '0' + nowMonth
                }
                if (strDate >= 0 && strDate <= 9) {
                    strDate = '0' + strDate
                }
                var nowDate = date.getFullYear() + seperator + nowMonth + seperator + strDate
                this.value = nowDate
            },
            search: function () {
                
                this.departDate = this.$refs.getValue.value
/*                console.log(this.departportCode)
                console.log(this.arrivalportCode)
                console.log(this.departDate)*/
                var cons = {}
                cons['depart_name'] = this.departportCode
                cons['arrive_name'] = this.arrivalportCode
                cons['date'] = this.departDate
                var json = JSON.stringify(cons)
                this.searchForm = json
               // alert('json数组为: ' + json)
                var _that = sb;
                _that.tableData = [];
                axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8'
                axios.post("/Flight/getFlight", { "depart_code": this.departportCode, "arrive_code": this.arrivalportCode, "dp_time": this.departDate }).then(res => {
                    console.log('res=>', res);
 
                    try {
                        var myflight = JSON.parse(res.data).data;
                        console.log(JSON.parse(res.data).data);
                        _that.tableData = JSON.parse(res.data).data;
                    }
                    catch (error) {
                        alert("暂无该航班信息");
                    }
                })

            },
            onChange1: function (value) {
                var length
                if (value[2] == null) length = 1
                else length = 2
                this.departportCode = value[length]
            },
            onChange2: function (value) {
                var length
                if (value[2] == null) length = 1
                else length = 2
                this.arrivalportCode = value[length]
            }
        }
    })
    var sb = new Vue({
    el: '#searchbox2',
        data() {
            return {
                tableData:[],
/*                tableData: [{
                    flightNo: 'FM9331',
                    departportName: '上海虹桥机场',
                    arrivalportName: '深圳宝安机场',
                    economyClassPrice: '1,070',
                    firstClassPrice: '2,690',
                    departTime: '08:05',
                    arrivalTime: '10:30',
                    companyName: '东方航空'
                }, {
                    flightNo: 'MU8349',
                    departportName: '上海虹桥机场',
                    arrivalportName: '深圳宝安机场',
                    economyClassPrice: '1,070',
                    firstClassPrice: '2,690',
                    departTime: '08:05',
                    arrivalTime: '10:30',
                    companyName: '东方航空'
                    }],*/
                options: [{
                    value: '经济舱',
                    label: '经济舱'
                }, {
                    value: '头等舱',
                    label: '头等舱'
                }],
                value: '',
                select:''
            }
        },
        created: function () {
            var _this = this;
            axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8'
            axios.post("/Flight/getSomeFlight", {}).then(res => {
                console.log('res=>', res);

                var myflight = JSON.parse(res.data).data;
                console.log(JSON.parse(res.data).data);
                _this.tableData = JSON.parse(res.data).data;
            })
        },
        methods: {
            selectCall(row) {
                console.log(row);
                this.select = row;
            },
            purchase: function (flightNo, companyName, departTime, arrivalTime, departport, arrivalport,fisrtClassPrice, economyClassPrice) {

                if (this.select == null)
                    return;
                else if (this.select == "经济舱") {  
                    url = "../Ticket/buyTicket?flightNo=" + flightNo + "&companyName=" + encodeURIComponent(companyName) + "&departTime=" + departTime + "&arrivalTime=" + arrivalTime + "&departport=" + encodeURIComponent(departport)
                        + "&arrivalport=" + encodeURIComponent(arrivalport) + "&flightClass=经济舱" + "&Price=" + economyClassPrice
                        ;//此处拼接内容
                    window.location.href = url;
                }
                else {
                        url = "../Ticket/buyTicket?flightNo=" + flightNo + "&companyName=" + encodeURIComponent(companyName) + "&departTime=" + departTime + "&arrivalTime=" + arrivalTime + "&departport=" + encodeURIComponent(departport)
                    + "&arrivalport=" + encodeURIComponent(arrivalport) + "&flightClass=头等舱" + "&Price=" + fisrtClassPrice
                        ;//此处拼接内容
                    window.location.href = url;
                }
            },
            
            change: function (flightNo, departTime) {
                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    if (pair[0] == "TicketID") { var ticketID = pair[1] }
                    else return false;
                }
                debugger
                if (this.select == null)
                    return;
                else if (this.select == "经济舱") 
                    axios.post("/Ticket/changeTicket1", {
                        ticket_ID: ticketID, flightNo: flightNo, departDate: departTime, flightClass: '经济舱'
                    }).then(res => {
                        console.log('res=>', res);
                        debugger
                        var state = JSON.parse(res.data).state;
                        if (state == 1) {
                            alert("改签成功！改签费用为"+JSON.parse(res.data).charge+'元。');
                            window.location.href = "/Ticket/books";
                        }
                        else {
                            alert("改签失败！");
                            window.location.href = "/Ticket/books";
                        }
                    })
                       
                else 
                    axios.post("/Ticket/changeTicket1", {
                        ticket_ID: ticketID, flightNo: flightNo, departDate: departTime, flightClass: '头等舱'
                    }).then(res => {
                        console.log('res=>', res);
                        var state = JSON.parse(res.data).state;
                        if (state == 1) {
                            alert("改签成功！改签费用为" + JSON.parse(res.data).charge + '元。');
                            window.location.href = "/Ticket/books";
                        }
                        else {
                            alert("改签失败！");
                            window.location.href = "/Ticket/books";
                        }
                    })
            }     
            }
    })






