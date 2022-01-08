import { selectBreakPoints } from '@abpdz/ng.theme.shared';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
@Component({
  selector: 'app-leafletmapview',
  templateUrl: './leafletmapview.component.html',
  styleUrls: ['./leafletmapview.component.scss'],
})
export class LeafletmapviewComponent implements AfterViewInit {
  private map;
  pos = [0, 0];
  gpsMarker = null;
  polyline;
  private initMap(): void {
    this.map = L.map('map', {
      center: this.pos,
      zoom: 10,
    });

    const tiles = L.tileLayer(
      // 'http://{s}.google.com/vt/lyrs=s,h&x={x}&y={y}&z={z}',
      'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      {
        maxZoom: 18,
        minZoom: 3,
        attribution: '&copy; <a href="">fateh Djehinet</a>',
      }
    );

    tiles.addTo(this.map);
    this.gpsMarker = L.marker(this.pos).addTo(this.map);
  }
  i = 0;
  constructor() {}
  add() {
    if (this.i == 0) {
      this.gpsMarker.setLatLng([36.81405466955785, 5.771343225485604]);
      this.i = 1;
    } else {
      this.gpsMarker.setLatLng([36.801947923222016, 5.844884529201847]);
      this.i = 0;
    }
  }
  getLocation() {}
  showPosition(position) {
    this.pos[0] = position.coords.latitude;
    this.pos[1] = position.coords.longitude;
    // alert(
    //   'Latitude: ' +
    //     position.coords.latitude +
    //     '<br>Longitude: ' +
    //     position.coords.longitude
    // );
  }
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
  }
  ngAfterViewInit(): void {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((p) => {
        this.pos = [p.coords.latitude, p.coords.longitude];
        this.initMap();
      });
    } else {
      alert('Geolocation is not supported by this browser.');
    }
  }
}
